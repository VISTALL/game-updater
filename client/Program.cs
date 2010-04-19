using System;
using System.Windows.Forms;
using com.jds.GUpdater.classes.assembly;
using com.jds.GUpdater.classes.config;
using com.jds.GUpdater.classes.forms;
using com.jds.GUpdater.classes.images;
using log4net;
using com.jds.GUpdater.classes;
using com.jds.GUpdater.classes.task_manager;
using com.jds.GUpdater.classes.language;
using System.Threading;

namespace com.jds.GUpdater
{
	public static class Program
	{
		private static readonly ILog _log = LogManager.GetLogger(typeof(Program)); 

		[STAThread]
		public static void Main()
		{
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var thread = Thread.CurrentThread;
                thread.Name = "GUpdater - MainThread";

                try
                {
                    LogService.Init();
                }
                catch (Exception)
                {
                    MessageBox.Show("Log4net config is not found");
                   // _log.Info("Exception[28]: " + e, e);
                }

                _log.Info("Program started.");

                TaskManager.Instance.Start();
                var a = AssemblyInfo.Instance;

                try
                {
                    RConfig.Instance.init();
                }
                catch (Exception e)
                {
                    _log.Info("Exception[41]: " + e, e);
                }

                try
                {
                    var languageHolder = LanguageHolder.Instance;
                    var h = ImageHolder.Instance;
                }
                catch (Exception e)
                {
                    _log.Info("Exception[50]: " + e, e);
                }

                try
                {
                    Application.Run(MainForm.Instance);

                    _log.Info("Program exit.");
                }
                catch (Exception e)
                {
                    _log.Info("Exception[61]: " + e, e);
                }
            }
            catch(Exception e)
            {
                _log.Info("Exception[72]: " + e, e);  
            }
		}
	}
}
