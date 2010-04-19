using System;
using System.Collections.Generic;
using System.Text;

namespace com.jds.Builder.classes.utils
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
