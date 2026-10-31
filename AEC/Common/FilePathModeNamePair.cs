using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Common
{
    public class FilePathModeNamePair
    {
        public readonly string filePath;
        public readonly string modeName;

        public FilePathModeNamePair(string path, string name)
        {
            this.filePath = path;
            this.modeName = name;
        }
    }
}
