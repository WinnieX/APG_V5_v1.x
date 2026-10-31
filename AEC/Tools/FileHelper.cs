using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AEC.Tools
{
    public class FileHelper
    {
        public static bool OpenFileLocation(string path)
        {
            if(System.IO.File.Exists(path))
            {
                try
                {
                    Process p = new Process();
                    p.StartInfo.FileName = "explorer.exe";
                    p.StartInfo.Arguments = String.Format(" /select, {0}", path);
                    p.Start();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void OpenDirectory(string dir)
        {
            if (System.IO.Directory.Exists(dir))
            {
                try
                {
                    Process p = new Process();
                    p.StartInfo.FileName = "explorer.exe";
                    p.StartInfo.Arguments = dir;
                    p.Start();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
