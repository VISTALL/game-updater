using System;
using System.Windows.Forms;
using com.jds.Premaker.classes;
using com.jds.Premaker.classes.forms;
using log4net;

namespace com.jds.Premaker
{
    static class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof (Program));

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LogService.Init();

            try
            {
                Application.Run(ChooseForm.Instance);
            }
            catch(Exception e)
            {
                _log.Info("Exception: " + e, e);
            }
        }

        public static String Version
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}
