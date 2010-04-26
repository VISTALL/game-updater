using System.IO;

namespace com.jds.AWLauncher.classes.utils
{
    public class FileUtils
    {
        public static bool IsFileOpen(FileInfo f)
        {
            if (!f.Exists)
            {
                return false;
            }
            bool rtnvalue = false;
            try
            {
                FileStream fs = f.OpenWrite();
                fs.Close();
            }
            catch (IOException)
            {
                rtnvalue = true;
            }
            return rtnvalue;
        }
    }
}