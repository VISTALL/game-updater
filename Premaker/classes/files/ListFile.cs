using System;
using System.IO;

namespace com.jds.Premaker.classes.files
{
    public class ListFile
    {       
        public ListFile()
        {
        }

        public void validateDirectoryes()
        {
            FileInfo f = new FileInfo(DescriptionDirectoryName + FileName);

            DirectoryInfo directory = f.Directory;
            
            while (directory != null)
            {
                if (!directory.Exists)
                {
                    directory.Create();
                }
                try
                {
                    directory = directory.Parent;
                }
                catch { }
            }
        }

        public FileStream CreateDescription()
        {
            String file = DescriptionDirectoryName + FileName + ".zip";
            FileInfo f = new FileInfo(file);

            if (f.Exists)
            {
                f.Delete();
            }

            EntryName = f.Name;

            return f.Create();
        }

        public FileStream CreateSource()
        {
            return File.OpenRead(SourceDirectoryName + FileName);
        }

        public DateTime SourceTime()
        {
            return File.GetLastWriteTime(SourceDirectoryName + FileName);
        }

        public bool Exists()
        {
            return File.Exists(SourceDirectoryName + FileName);
        }

        #region Properies
        public String EntryName
        {
            get;
            set;
        }

        public String FileName
        {
            get;
            set;
        }

        public String DescriptionDirectoryName
        {
            get;
            set;
        }

        public String SourceDirectoryName
        {
            get;
            set;
        }

        public bool IsCritical
        {
            get;
            set;
        }

        public String MD5
        {
            get
            {
                return DTHasher.GetMD5Hash(SourceDirectoryName + FileName);
            }
        }

        #endregion
    }
}
