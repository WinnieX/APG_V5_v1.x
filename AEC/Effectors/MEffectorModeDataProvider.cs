using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Common;

namespace AEC.Effectors
{
    public abstract class MEffectorModeDataProvider
    {
        public string name
        {
            get;
            private set;
        }

        public MEffectorModeDataProvider(string name_)
        {
            Debug.Assert(!String.IsNullOrEmpty(name_));
            this.name = name_;
        }

        public abstract EffectorModelBase Deseralize(MEffectorSeralizer effectorSeralizer);

        public abstract bool isCustomData { get; }
    }

    public class MFactoryModeProvider : MEffectorModeDataProvider
    {
        private readonly byte[] buffer;

        public MFactoryModeProvider(string name_, byte[] buffer_) :
            base(name_)
        {
            Debug.Assert(buffer_ != null);
            this.buffer = buffer_;
        }

        public override EffectorModelBase Deseralize(MEffectorSeralizer effectorSeralizer)
        {
            return effectorSeralizer.DeseralizeToCurrEffectorFromBuffer(this.buffer, false);
        }

        public override bool isCustomData
        {
            get
            {
                return false;
            }
        }
    }

    public class MCustomModeProvider : MEffectorModeDataProvider, IComparable<MCustomModeProvider>, IEquatable<MCustomModeProvider>
    {
        public readonly string path;
        public readonly string mainModel; //当前型号
        public readonly string folder;    //所在文件夹

        public MCustomModeProvider(string name_, string path_, string mainModel_, string folder_)
            :base(name_)
        {
            Debug.Assert(!String.IsNullOrEmpty(path_));
            this.path = path_;
            this.mainModel = mainModel_;
            this.folder = folder_;
        }

        public bool isExternal
        {
            get
            {
                return this.mainModel != this.folder;
            }
        }

        public override EffectorModelBase Deseralize(MEffectorSeralizer effectorSeralizer)
        {
            return effectorSeralizer.DeseralizeToCurrEffectorFromFile(this.path, false);
        }

        public override bool isCustomData
        {
            get
            {
                return true;
            }
        }

        public int CompareTo(MCustomModeProvider other)
        {
            if(other == null)
            {
                return 1;
            }

            try
            {
                System.IO.FileInfo fileInfo1 = new System.IO.FileInfo(this.path);
                System.IO.FileInfo fileInfo2 = new System.IO.FileInfo(other.path);
                DateTime fileTime1 = fileInfo1 == null ? new DateTime() : fileInfo1.LastWriteTime;
                DateTime fileTime2 = fileInfo2 == null ? new DateTime() : fileInfo2.LastWriteTime;
                if (fileTime1 > fileTime2) return -1;
                if (fileTime1 < fileTime2) return 1;
            }
            catch(Exception )
            {

            }

            return 0;
        }

        public bool Equals(MCustomModeProvider other)
        {
            if(other == null)
            {
                return false;
            }

            try
            {
                System.IO.FileInfo fileInfo1 = new System.IO.FileInfo(this.path);
                System.IO.FileInfo fileInfo2 = new System.IO.FileInfo(other.path);
                DateTime fileTime1 = fileInfo1 == null ? new DateTime() : fileInfo1.LastWriteTime;
                DateTime fileTime2 = fileInfo2 == null ? new DateTime() : fileInfo2.LastWriteTime;

                return fileTime1 == fileTime2;
            }
            catch (Exception)
            {

            }

            return false;
        }
    }
}
