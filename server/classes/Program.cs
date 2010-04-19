using System;
using System.Collections.Generic;
using System.Windows.Forms;
using log4net;
using com.jds.Builder.classes.forms;
using System.IO;

namespace com.jds.Builder.classes
{
    public class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program)); 

        [STAThread]
        static void Main()
        {
            LogService.init();           

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            _log.Info("Program started");

            try
            {
                MainForm form = MainForm.Instance;

                Application.Run(form);
            }
            catch (Exception e)
            {
                _log.Info("Exception: " + e, e);
            }
            finally
            {
                _log.Info("Program exit");
            }
        }
    }
}
