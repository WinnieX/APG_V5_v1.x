using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace AEC.Common
{
    public class ULLPasswordGroup
    {
        public readonly string[] passwords = new string[SystemModuleBase.DefUsageLimitLockCount];

        public ULLPasswordGroup()
        {
            for(int i = 0; i < this.passwords.Length; ++i)
            {
                this.passwords[i] = string.Empty;
            }
        }

        public ULLPasswordGroup(string[] passwords_)
        {
            this.Update(passwords_);
        }

        public void Update(string[] passwords_)
        {
            Debug.Assert(passwords_ != null && passwords_.Length == passwords.Length);
            for (int i = 0; i < passwords_.Length; ++i)
            {
                Debug.Assert(passwords_[i] != null && (passwords_[i].Length == 0 || passwords_[i].Length == UsageLimitLock.PasswordLength));

                if (passwords_[i].Length == UsageLimitLock.PasswordLength)
                {
                    uint temp = 0;
                    Debug.Assert(uint.TryParse(passwords_[i], out temp));
                }
            }

            for (int i = 0; i < this.passwords.Length; ++i)
            {
                this.passwords[i] = passwords_[i];
            }
        }
    }

    public class ULLPasswordGroupSeralizer
    {
        public const string DefaultDir = @"M-Audio\AEC\UsageLimitLock\Passwords";

        public const string DEF_MLP_FILE_FILTER = "mlp File|*.mlp";
        public const string MLP_FILE_EXTENSION = ".mlp";

        private const string RootNode = "mlp";

        private const string versionName = "version";
        private const string version = "1.0";

        private const string passwordNode = "password";
        private const string IndexAttr = "index";

        public static string InitialDirectory
        {
            get
            {
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dir = Path.Combine(dir, DefaultDir);
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                return dir;
            }
        }

        public static void SeralizeToFile(ULLPasswordGroup passwordGroup, string path)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);

            XmlElement rootEle = xmldoc.CreateElement(RootNode);
            rootEle.SetAttribute(versionName, version);

            for (int i = 0; i < passwordGroup.passwords.Length; ++i)
            {
                rootEle.AppendChild(CreatePasswordNode(xmldoc, passwordGroup.passwords[i], i));
            }

            xmldoc.AppendChild(rootEle);

            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, new UTF8Encoding(false));
            xmldoc.Save(sw);
            sw.Close();
        }

        private static XmlElement CreatePasswordNode(XmlDocument xmldoc, string password, int index)
        {
            XmlElement ele = xmldoc.CreateElement(passwordNode);
            ele.SetAttribute(IndexAttr, index.ToString());

            ele.InnerText = password;

            return ele;
        }

        public static ULLPasswordGroup DeseralizeFromFile(string path, out string errorMsg)
        {
            errorMsg = string.Empty;

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return DeseralizeFromStream(fs, out errorMsg);
            }
        }

        public static ULLPasswordGroup DeseralizeFromStream(Stream stream, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                ULLPasswordGroup pg = new ULLPasswordGroup();

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(stream);

                if (xmldoc.DocumentElement != null &&
                xmldoc.DocumentElement.LocalName == RootNode)
                {
                    XmlElement rootNode = xmldoc.DocumentElement;

                    if (rootNode.HasAttribute(versionName) &&
                        rootNode.GetAttribute(versionName) == version)
                    {
                        XmlNodeList passwordNodes = rootNode.SelectNodes(passwordNode);
                        foreach (XmlNode node in passwordNodes)
                        {
                            XmlElement passwordEle = node as XmlElement;

                            int index = ParseText2Int(passwordEle.GetAttribute(IndexAttr));
                            if (index < 0 || index >= pg.passwords.Length)
                            {
                                return null;
                            }

                            if(passwordEle.InnerText.Length == UsageLimitLock.PasswordLength &&
                                UsageLimitLockHelper.IsValidPassword(passwordEle.InnerText))
                            {
                                pg.passwords[index] = passwordEle.InnerText;
                            }                            
                        }

                        return pg;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = string.Format("DeseralizeFromStream()...Exception:{0}", ex.Message);
                System.Diagnostics.Debug.Print(ex.Message);
            }

            return null;
        }

        private static int ParseText2Int(string txt)
        {
            int value;
            if (int.TryParse(txt, out value))
            {
                return value;
            }

            return -1;
        }
    }
}
