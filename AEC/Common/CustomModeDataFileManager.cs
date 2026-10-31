using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.IO;
using AEC.Effectors;

namespace AEC.Common
{
    public class CustomModeDataFileManager
    {
        public const string BackupFileExtensionName = ".xml";
        //public const string ModeDataDir = @"Mode\";
        public const string PresetDataDir = @"Mode\";

        private readonly string startupPath = string.Empty;
        private readonly Dictionary<string, string> presetDataDirMap = new Dictionary<string, string>();
        private readonly Dictionary<string, List<string>> presetDataFilesMap = new Dictionary<string, List<string>>();

        public CustomModeDataFileManager(string startupPath_)
        {
            Debug.Assert(startupPath_ != null);

            this.startupPath = startupPath_;
            if(!System.IO.Directory.Exists(startupPath))
            {
                startupPath = System.AppDomain.CurrentDomain.BaseDirectory;
            }

            if(!startupPath.EndsWith(@"\"))
            {
                startupPath += @"\";
            }
        }

        public string GetPresetDataFileDir(string model)
        {
            if (!this.presetDataDirMap.ContainsKey(model))
            {
                string presetDir = this.startupPath + PresetDataDir + model + @"\";
                this.presetDataDirMap[model] = presetDir;

                try
                {
                    if (!Directory.Exists(presetDir))
                    {
                        Directory.CreateDirectory(presetDir);
                    }
                }
                catch
                {

                }
            }

            return this.presetDataDirMap[model];
        }

//        public string GetModeFileFullPath(string model, string name)
//        {
//            string modeDir = this.GetModeDataFileDir(model);

//#if COMM_TEST
//            modeDir += @"CommTest\";
//            if (!Directory.Exists(modeDir))
//            {
//                try
//                {
//                    Directory.CreateDirectory(modeDir);
//                }
//                catch(Exception )
//                {

//                }
//            }
//#endif

//            return modeDir + name + BackupFileExtensionName;
//        }

        public string GetPresetFileFullPath(string model, String name)
        {
            string modeDir = this.GetPresetDataFileDir(model);

#if COMM_TEST
            modeDir += @"CommTest\";
            if (!Directory.Exists(modeDir))
            {
                try
                {
                    Directory.CreateDirectory(modeDir);
                }
                catch(Exception )
                {

                }
            }
#endif

            return modeDir + name + BackupFileExtensionName;
        }

        public void UpdateModeFile(string model, string name)
        {
            List<string> fileList = GetModeFileNameList(model);
            if (fileList.Contains(name))
            {
                fileList.Remove(name);
            }
            fileList.Insert(0, name);
        }

        public void DeleteModeFile(string model, string name)
        {
            string filePath = GetPresetFileFullPath(model, name);
            List<string> fileList = GetModeFileNameList(model);
            if(fileList.Contains(name))
            {
                fileList.Remove(name);
            }
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch (Exception)
                {

                }
            }
        }

        private List<string> GetModeFileNameList(string model)
        {
            if (!this.presetDataFilesMap.ContainsKey(model))
            {
                string modeDir = this.GetPresetDataFileDir(model);
                List<string> modeFileList = new List<string>();

                try
                {
                    string[] files = System.IO.Directory.GetFiles(modeDir, "*" + BackupFileExtensionName);

                    foreach (string file in files)
                    {
                        string name = System.IO.Path.GetFileNameWithoutExtension(file).Trim();
                        if (!string.IsNullOrEmpty(name))
                        {
                            modeFileList.Add(name);
                        }
                    }
                }
                catch
                {

                }

                this.presetDataFilesMap[model] = modeFileList;
            }

            return this.presetDataFilesMap[model];
        }

        public MCustomModeProvider[] GetCustomModeProviderList(string[] models, bool updateCache)
        {
            Debug.Assert(models != null && models.Length >= 1);

            List<MCustomModeProvider> customModeList = new List<MCustomModeProvider>();

            if (updateCache)
            {
                this.presetDataFilesMap.Clear();
            }

            foreach(string model in models)
            {
                List<string> fileNames = GetModeFileNameList(model);

                foreach (string fileName in fileNames)
                {
                    string filePath = GetPresetFileFullPath(model, fileName);

                    customModeList.Add(new MCustomModeProvider(fileName, filePath, models[0], model));
                }
            }

            customModeList.Sort();

            return customModeList.ToArray();
        }

        //public List<string> GetPresetFileList(string model)
        //{
        //    if (!this.presetDataFilesMap.ContainsKey(model))
        //    {
        //        string presetDir = this.GetPresetDataFileDir(model);
        //        List<string> presetFileList = new List<string>();

        //        try
        //        {
        //            string[] files = System.IO.Directory.GetFiles(presetDir, "*" + BackupFileExtensionName);
        //            IComparer timeComparer = new TimeComparer();
        //            Array.Sort(files, timeComparer);

        //            foreach (string file in files)
        //            {
        //                string name = System.IO.Path.GetFileNameWithoutExtension(file);
        //                presetFileList.Add(name);
        //            }
        //        }
        //        catch
        //        {

        //        }

        //        this.presetDataFilesMap[model] = presetFileList;
        //    }

        //    return this.presetDataFilesMap[model];
        //}

        //class TimeComparer : IComparer
        //{
        //    public int Compare(object info1, object info2)
        //    {
        //        FileInfo fileInfo1 = new FileInfo((string)info1);
        //        FileInfo fileInfo2 = new FileInfo((string)info2);
        //        DateTime fileTime1 = fileInfo1 == null ? new DateTime() : fileInfo1.LastWriteTime;
        //        DateTime fileTime2 = fileInfo2 == null ? new DateTime() : fileInfo2.LastWriteTime;
        //        if (fileTime1 > fileTime2) return -1;
        //        if (fileTime1 < fileTime2) return 1;

        //        return 0;
        //    }
        //}
    }
}
