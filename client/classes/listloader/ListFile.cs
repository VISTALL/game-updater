using System;
using com.jds.AWLauncher.classes.listloader.enums;

namespace com.jds.AWLauncher.classes.listloader
{
    public class ListFile
    {
        private String _fileName;
        private String _md5;
        private ListFileType _type;

        #region Property

        public String md5Checksum
        {
            get { return _md5; }
        }

        public String FileName
        {
            get { return _fileName; }
        }

        public ListFileType Type
        {
            get { return _type; }
        }

        #endregion

        #region Methods

        public static ListFile parse(String line)
        {
            var file = new ListFile();
            String[] args = line.Split('\t');
            file._fileName = args[0];
            file._type = Boolean.Parse(args[1]) ? ListFileType.CRITICAL : ListFileType.NORMAL;
            file._md5 = args[2];
            return file;
        }
        
        public static ListFile parseIgnoreCritical(String line)
        {
            var file = new ListFile();
            String[] args = line.Split('\t');
            file._fileName = args[0];
            file._md5 = args[1];
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