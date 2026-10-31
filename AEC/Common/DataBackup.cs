using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace AEC.Common
{
    public class DataBackup
    {
        public const string BackupFileExtensionName = ".xml";
        public const string X5BackupFileExtensionName = ".xom";
        private readonly string modeDir = string.Empty;
        private readonly string presetDataDir = string.Empty;
        private List<String> _nameList = new List<string>();
        private List<String> _presetPathList = new List<string>();

        public List<String> nameList
        {
            get
            {
                return _nameList;
            }
        }

        public List<string> filePathList
        {
            get
            {
                List<string> pathList = new List<string>();
                foreach(string name in _nameList)
                {
                    pathList.Add(GetModeFileFullPath(name));
                }
                return pathList;
            }
        }

        public List<string> presetPathList
        {
            get
            {
                return _presetPathList;
            }
        }

        public DataBackup(string deviceModel_, string startupPath)
        {
            Debug.Assert(!string.IsNullOrEmpty(deviceModel_) && startupPath != null);
            if(!System.IO.Directory.Exists(startupPath))
            {
                startupPath = System.AppDomain.CurrentDomain.BaseDirectory;
            }
            if(!startupPath.EndsWith(@"\"))
            {
                startupPath += @"\";
            }
            modeDir = startupPath + @"Mode\" + deviceModel_ + @"\";
            presetDataDir = startupPath + @"Preset\" + deviceModel_ + @"\";

            //if (System.IO.Directory.Exists(modeDir))
            //{
            //    try
            //    {
            //        string[] files = System.IO.Directory.GetFiles(modeDir, "*" + BackupFileExtensionName);
            //        IComparer timeComparer = new TimeComparer();
            //        Array.Sort(files, timeComparer);

            //        foreach(string file in files)
            //        {
            //            string name = System.IO.Path.GetFileNameWithoutExtension(file);
            //            _nameList.Add(name);
            //        }
            //    }
            //    catch
            //    {

            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        System.IO.Directory.CreateDirectory(modeDir);
            //    }
            //    catch
            //    {

            //    }
            //}

            //if (System.IO.Directory.Exists(presetDataDir))
            //{
            //    try
            //    {
            //        string[] filesPath = System.IO.Directory.GetFiles(presetDataDir, "*" + BackupFileExtensionName);
            //        IComparer timeComparer = new TimeComparer();
            //        Array.Sort(filesPath, timeComparer);
            //        _presetPathList.AddRange(filesPath);
            //    }
            //    catch
            //    {

            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        System.IO.Directory.CreateDirectory(presetDataDir);
            //    }
            //    catch
            //    {

            //    }
            //}
        }

        public string GetModeFileFullPath(String name)
        {
            return modeDir + name + BackupFileExtensionName;
        }

        public string GetPresetFileFullPath(String name)
        {
            return presetDataDir + name + BackupFileExtensionName;
        }

        public string GetModeDir()
        {
            return modeDir;
        }

        public string GetPresetModeDir()
        {
            return presetDataDir;
        }

        public static bool CheckFileExtensionIsOk(string path)
        {
            string extension = System.IO.Path.GetExtension(path);
            return extension == DataBackup.BackupFileExtensionName || extension == DataBackup.X5BackupFileExtensionName;
        }

        public static bool CheckBackupFileIsX5(string path)
        {
            return System.IO.Path.GetExtension(path) == X5BackupFileExtensionName;
        }
    }

    class TimeComparer : IComparer
    {
        public int Compare(object info1, object info2)
        {
            FileInfo fileInfo1 = new FileInfo((string)info1);
            FileInfo fileInfo2 = new FileInfo((string)info2);
            DateTime fileTime1 = fileInfo1 == null ? new DateTime() : fileInfo1.LastWriteTime;
            DateTime fileTime2 = fileInfo2 == null ? new DateTime() : fileInfo2.LastWriteTime;
            if (fileTime1 > fileTime2) return -1;
            if (fileTime1 < fileTime2) return 1;

            return 0;
        }
    }
}
