using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using log4net.Config;

namespace com.jds.AWLauncher.classes
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
                MessageBox.Show("Log4net config is not found");
                Process.GetCurrentProcess().Kill();
            }

            XmlConfigurator.Configure(new FileInfo("log4net.xml"));
        }
    }
}