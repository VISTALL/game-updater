using System;
using System.IO;
using log4net.Config;

namespace com.jds.GUpdater.classes
{
    /**
     * Author: VISTALL
     * Company: J Develop Station
     * Time: 16:37 /1.03.2010
     */

    public class LogService
    {
        public static void Init()
        {
            if (!File.Exists("log4net.xml"))
            {
                throw new Exception("Log4net config is not found");
            }
            XmlConfigurator.Configure(new FileInfo("log4net.xml"));
        }
    }
}