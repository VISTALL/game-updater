using System;

namespace com.jds.Premaker.classes.utiles
{
    public class StringUtil
    {
        public static void slashes(String path, out String outt)
        {
            outt = path;
            outt = outt.Replace("\\", "/");
        }
    }
}
