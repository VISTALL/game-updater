using System;
using System.Windows.Forms;
using System.Threading;

using com.jds.AWLauncher.classes;
using com.jds.AWLauncher.classes.config;
using com.jds.AWLauncher.classes.forms;
using com.jds.AWLauncher.classes.images;
using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.task_manager;
using com.jds.AWLauncher.classes.version_control;

using log4net;

namespace com.jds.AWLauncher
{
	public static class Program
	{
		private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

		[STAThread]
		public static void Main()
		{
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); 
            
            try
            {
                var thread = Thread.CurrentThread;
                thread.Name = "AWLauncher - MainThread";

                LogService.Init();

                _log.Info("Program started.");

                TaskManager.Instance.Start();
                
                RConfig.Instance.init();
               

                LanguageHolder.Instance();
                ImageHolder.Instance();

                var mainForm = MainForm.Instance;

                AssemblyInfo.Instance();
                PropertyForm.Instance();

               Application.Run(mainForm);

               _log.Info("Program exit.");
            }
            catch(Exception e)
            {
                new ExceptionForm(e);
                //_log.Info("Exception[84]: " + e, e);  
            }
		}
	}
}
