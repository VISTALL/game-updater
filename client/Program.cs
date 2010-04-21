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

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]

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
                   // MessageBox.Show("Log4net config is not found");
                   //_log.Info("Exception[28]: " + e, e);
                }

                _log.Info("Program started.");

                TaskManager.Instance.Start();
                
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
                    LanguageHolder.Instance();
                    ImageHolder.Instance();
                }
                catch (Exception e)
                {
                    _log.Info("Exception[50]: " + e, e);
                }

                var mainForm = MainForm.Instance;
                AssemblyInfo.Instance();
                PropertyForm.Instance();

                try
                {
                    Application.Run(mainForm);

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
