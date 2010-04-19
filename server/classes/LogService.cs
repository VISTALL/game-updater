using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using log4net.Config;

namespace com.jds.Builder.classes
{
    public class LogService
    {
        public static void init()
        {
            File.Delete("log.txt"); 
            
            if (!File.Exists("log4net.xml"))
            {
                throw new Exception("Log4net config is not found");
            }
            XmlConfigurator.Configure(new FileInfo("log4net.xml"));
        }
    }
}
