using System;
using com.jds.AWLauncher.classes.listloader.enums;

namespace com.jds.AWLauncher.classes.listloader
{
    public class ListFile
    {
        #region Property

        public string md5Checksum { get; private set; }

        public string FileName { get; private set; }

        public ListFileType Type { get; private set; }

        #endregion

        #region Methods

        public static ListFile parse(String line)
        {
            var file = new ListFile();
            String[] args = line.Split('\t');
            file.FileName = args[0];
            file.Type = (ListFileType)Enum.Parse(typeof(ListFileType), args[1]);
            file.md5Checksum = args[2];
            return file;
        }
        
        public static ListFile parseIgnoreCritical(String line)
        {
            var file = new ListFile();
            String[] args = line.Split('\t');
            file.FileName = args[0];
            file.md5Checksum = args[1];
            return file;
        }
        #endregion

        #region Overrides

        public override string ToString()
        {
            return String.Format("File: name {0} md5 {1} type {2}", FileName, md5Checksum, Type);
        }

        #endregion
    }
}