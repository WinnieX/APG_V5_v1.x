using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using AEC.Core;

namespace AEC.Common
{
    public class ULLOperation
    {
        public bool isUpdate { get; set; }
        public string oldPassword { get; set; }
        public readonly UsageLimitLock ull;

        public int index
        {
            get
            {
                return this.ull.index;
            }
        }

        public bool enable
        {
            get
            {
                return this.ull.enable;
            }
            set
            {
                this.ull.enable = value;
            }
        }

        public ushort usageCount
        {
            get
            {
                return this.ull.usageCount;
            }
            set
            {
                this.ull.usageCount = value;
            }
        }

        public string newPassword
        {
            get
            {
                return this.ull.systemPasswordStr;
            }
            set
            {
                this.ull.systemPasswordStr = value;
            }
        }

        public ULLOperation(short index_)
        {
            this.isUpdate = false;
            this.oldPassword = string.Empty;
            this.ull = new UsageLimitLock(index_);
        }

        public bool CheckIsOK(out string error)
        {
            error = string.Empty;

            if(string.IsNullOrEmpty(this.oldPassword) || oldPassword.Length != UsageLimitLock.PasswordLength)
            {
                return false;
            }

            return true;
        }
    }

    public class ULLTask
    {
        public readonly ULLOperation[] ullOperations = null;

        public int updateCount
        {
            get
            {
                int count = 0;
                foreach(var operation in ullOperations)
                {
                    if(operation.isUpdate)
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        public string[] oldPasswords
        {
            get
            {
                string[] password = new string[this.ullOperations.Length];
                for(int i = 0; i < password.Length; ++i)
                {
                    password[i] = ullOperations[i].oldPassword;
                }

                return password;
            }
        }

        public ushort[] usageCounts
        {
            get
            {
                ushort[] usages = new ushort[this.ullOperations.Length];
                for (int i = 0; i < usages.Length; ++i)
                {
                    usages[i] = ullOperations[i].usageCount;
                }

                return usages;
            }
        }

        public string[] newPasswords
        {
            get
            {
                string[] password = new string[this.ullOperations.Length];
                for (int i = 0; i < password.Length; ++i)
                {
                    password[i] = ullOperations[i].newPassword;
                }

                return password;
            }
        }
        
        public ULLTask()
        {
            this.ullOperations = new ULLOperation[SystemModuleBase.DefUsageLimitLockCount];
            for(short index = 0; index < SystemModuleBase.DefUsageLimitLockCount; ++index)
            {
                this.ullOperations[index] = new ULLOperation(index);
            }
        }
    }

    public class ULLTaskSeralizer
    {
        public const string DefaultDir = @"M-Audio\AEC\UsageLimitLock\Locks";

        public const string DEF_MLT_FILE_FILTER = "mlt File|*.mlt";
        public const string MLT_FILE_EXTENSION = ".mlt";

        private const string RootNode = "mlt";

        private const string versionName = "version";
        private const string version = "1.0";

        private const string LockOperationNode = "lock_operation";
        private const string IndexAttr = "index";
        private const string nameAttr = "name";
        private const string dataNode = "data";
        private const string isUpdateName = "is_update";
        private const string oldPasswordName = "old_password";
        private const string isEnableName = "is_enable";
        private const string usageCountName = "usage_count";
        private const string newPasswordName = "new_password";

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

        public static void SeralizeToFile(ULLTask task, string path)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);

            XmlElement rootEle = xmldoc.CreateElement(RootNode);
            rootEle.SetAttribute(versionName, version);

            foreach(var operation in task.ullOperations)
            {
                rootEle.AppendChild(CreateULLOperationNode(xmldoc, operation));
            }

            xmldoc.AppendChild(rootEle);

            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, new UTF8Encoding(false));
            xmldoc.Save(sw);
            sw.Close();
        }

        private static XmlElement CreateULLOperationNode(XmlDocument xmldoc, ULLOperation operation)
        {
            XmlElement ele = xmldoc.CreateElement(LockOperationNode);
            ele.SetAttribute(IndexAttr, operation.index.ToString());

            ele.AppendChild(CreateDataNode(xmldoc, DataConverter.Bool2Short(operation.isUpdate), isUpdateName));
            ele.AppendChild(CreateDataNode(xmldoc, operation.oldPassword, oldPasswordName));

            ele.AppendChild(CreateDataNode(xmldoc, DataConverter.Bool2Short(operation.enable), isEnableName));
            ele.AppendChild(CreateDataNode(xmldoc, operation.usageCount, usageCountName));
            ele.AppendChild(CreateDataNode(xmldoc, operation.newPassword, newPasswordName));

            return ele;
        }

        private static XmlElement CreateDataNode(XmlDocument xmldoc, short data, string name)
        {
            return CreateDataNode(xmldoc, data.ToString(), name);
        }

        private static XmlElement CreateDataNode(XmlDocument xmldoc, ushort data, string name)
        {
            return CreateDataNode(xmldoc, data.ToString(), name);
        }

        private static XmlElement CreateDataNode(XmlDocument xmldoc, string data, string name)
        {
            XmlElement ele = xmldoc.CreateElement(dataNode);
            ele.SetAttribute(nameAttr, name);
            ele.InnerText = data;

            return ele;
        }

        public static ULLTask DeseralizeFromFile(string path, out string errorMsg)
        {
            errorMsg = string.Empty;

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return DeseralizeFromStream(fs, out errorMsg);
            }
        }

        public static ULLTask DeseralizeFromStream(Stream stream, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                ULLTask task = new ULLTask();

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(stream);

                if (xmldoc.DocumentElement != null &&
                xmldoc.DocumentElement.LocalName == RootNode)
                {
                    XmlElement rootNode = xmldoc.DocumentElement;

                    if (rootNode.HasAttribute(versionName) &&
                        rootNode.GetAttribute(versionName) == version)
                    {
                        XmlNodeList lockNodes = rootNode.SelectNodes(LockOperationNode);
                        foreach (XmlNode lockNode in lockNodes)
                        {
                            XmlElement lockEle = lockNode as XmlElement;

                            int index = ParseText2Int(lockEle.GetAttribute(IndexAttr));
                            if (index < 0 || index >= task.ullOperations.Length)
                            {
                                return null;
                            }

                            ParseULLOperationElement(lockEle, task.ullOperations[index]);
                        }
                    }

                    return task;
                }
            }
            catch (Exception ex)
            {
                errorMsg = string.Format("DeseralizeFromStream()...Exception:{0}", ex.Message);
                System.Diagnostics.Debug.Print(ex.Message);
            }

            return null;
        }

        private static void ParseULLOperationElement(XmlElement lockEle, ULLOperation lockOperation)
        {
            foreach (XmlElement ele in lockEle.ChildNodes)
            {
                if (ele.Name == dataNode)
                {
                    string name = ele.GetAttribute(nameAttr);
                    if(name == oldPasswordName)
                    {
                        if (UsageLimitLockHelper.IsValidPassword(ele.InnerText))
                        {
                            lockOperation.oldPassword = ele.InnerText;
                        }
                    }
                    else if (name == newPasswordName)
                    {
                        if (UsageLimitLockHelper.IsValidPassword(ele.InnerText))
                        {
                            lockOperation.newPassword = ele.InnerText;
                        }
                    }
                    else if (name == isUpdateName)
                    {
                        int data = ParseText2Int(ele.InnerText);
                        if(data > 0)
                        {
                            lockOperation.isUpdate = data != 0;
                        }                        
                    }
                    else if (name == isEnableName)
                    {
                        int data = ParseText2Int(ele.InnerText);
                        if (data > 0)
                        {
                            lockOperation.enable = data != 0;
                        }
                    }
                    else if (name == usageCountName)
                    {
                        int data = ParseText2Int(ele.InnerText);
                        if (data > 0)
                        {
                            lockOperation.usageCount = (ushort)data;
                        }
                    }
                }
            }
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
