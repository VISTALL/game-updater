using System;
using System.Windows.Forms;
using com.jds.AWLauncher.classes;
using com.jds.AWLauncher.classes.config;
using com.jds.AWLauncher.classes.forms;
using com.jds.AWLauncher.classes.images;
using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.task_manager;
using com.jds.AWLauncher.classes.version_control;
using log4net;
using System.Threading;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace com.jds.AWLauncher
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
                    return; 
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
