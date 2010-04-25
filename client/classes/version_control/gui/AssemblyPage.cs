#region Usage
using System;
using System.IO;
using System.Windows.Forms;
using com.jds.GUpdater.classes.forms;
using com.jds.GUpdater.classes.invoke;
using com.jds.GUpdater.classes.language;
using com.jds.GUpdater.classes.language.enums;
using com.jds.GUpdater.classes.listloader;
using com.jds.GUpdater.classes.listloader.enums;
using com.jds.GUpdater.classes.task_manager;
using com.jds.GUpdater.classes.task_manager.tasks;
using com.jds.GUpdater.classes.utils;
using com.jds.GUpdater.classes.windows.windows7;
using log4net;
#endregion

namespace com.jds.GUpdater.classes.version_control.gui
{
    public partial class AssemblyPage : UserControl
    {
        #region Instance & Constructor & Variables

        private readonly GUListLoaderTask _listLoaderTask = new GUListLoaderTask();
        private readonly ILog _log = LogManager.GetLogger(typeof (AssemblyPage));
        public readonly InvokeManager InvokeManager = new InvokeManager(typeof(AssemblyPage));

        private static AssemblyPage _instance;
       
        public  static  AssemblyPage Instance()
        {
            return _instance ?? (_instance = new AssemblyPage());
        }

        private AssemblyPage()
        {
            InitializeComponent();
            ChangeLanguage();
            _version.Text = AssemblyInfo.Instance().AssemblyVersion;
           // 
        }

        #endregion

        #region Check Method
        private void _checkButton_Click(object sender, EventArgs e)
        {
            switch(FState)
            {
                case MainFormState.NONE:
                    TaskManager.Instance.AddTask(_listLoaderTask);
                    break;
                case MainFormState.CHECKING:
                    TaskManager.Instance.Close(false);
                    break;
            }
        }

        private void _updateBtn_Click(object sender, EventArgs e)
        {
            switch (FState)
            {
               case MainFormState.NONE:
                    TaskManager.Instance.AddTask(new GUAnalyzerTask());
                   break;
                case MainFormState.CHECKING:

                    break;
                case MainFormState.DONE:
                   
                    foreach (ListFile f in _listLoaderTask.Items)
                    {
                        var fileName = Directory.GetCurrentDirectory() + f.FileName;
                        var oldFileName = Directory.GetCurrentDirectory() + f.FileName + ".old";
                        var newFileName = Directory.GetCurrentDirectory() + f.FileName + ".new";
                        try
                        {

                            if (File.Exists(newFileName))
                            {
                                if (File.Exists(fileName))
                                {
                                    if (File.Exists(oldFileName))
                                    {
                                        File.Delete(oldFileName);
                                    }

                                    File.Move(fileName, fileName + ".old");
                                }

                                File.Move(newFileName, fileName);
                            }
                        }
                        catch(Exception e1)
                        {
                            _log.Info("Exception: "+ e1.Message, e1);   
                        }
                    }
                    
                    Application.Restart();;
                    break;
            }
        }
        #endregion

        #region Update Status Label
        
        public void UpdateStatusLabelUnsafe(String a)
        {
            _statusLabel.Text = a;
        }
        
        public void UpdateStatusLabel(WordEnum a)
        {
            UpdateStatusLabel(LanguageHolder.Instance()[a]);
        }

        public void UpdateStatusLabel(String a)
        {
            var delegateCall = new DelegateCall(this, new MainForm.UpdateStatusLabelDelegate(UpdateStatusLabelUnsafe), a);

            Invoke(delegateCall);    
        }
        #endregion

        #region State
        public MainFormState FState { get; set; }

        public void SetState(MainFormState type)
        {
            var d = new DelegateCall(this, new MainForm.SetFormStateDelegate(SetStateUnsafe), type);
            Invoke(d);
        }

        private void SetStateUnsafe(MainFormState s)
        {
            switch (s)
            {
                case MainFormState.NONE:
                    _checkButton.Text = LanguageHolder.Instance()[WordEnum.CHECK];
                    //_updateBtn.Enabled = trueInstance()
                    _updateBtn.Text = LanguageHolder.Instance()[WordEnum.UPDATE];  
                    break;
                case MainFormState.CHECKING:
                    _checkButton.Text = LanguageHolder.Instance()[WordEnum.CANCEL];  
                    _updateBtn.Enabled = false;
                    break;
                case MainFormState.DONE:
                    _updateBtn.Enabled = true;
                    _updateBtn.Text = LanguageHolder.Instance()[WordEnum.RESTART];  
                    break;
            }

            MainForm.Instance.SetMainFormState(s == MainFormState.DONE ? MainFormState.NONE : s);

            FState = s;
        }
        #endregion

        #region Version Type

        public void SetVersionType(String va, VersionType a)
        {
            var d = new DelegateCall(this, new MainForm.SetVersionTypeDelegate(SetVersionTypeU), va,  a);
           
            Invoke(d);
        }

        public void SetVersionTypeU(String va, VersionType t)
        {
            _currentVersion.Text = va;

            //_versionTypeLabel.Text = _versionType.ToString();
            
            _versionTypeLabel.Text =
                LanguageHolder.Instance()[
                    (WordEnum) Enum.Parse(typeof (WordEnum), string.Format("{0}_VERSION", t))];

            switch (t)
            {
                case VersionType.BIGGER:
                case VersionType.SAME:
                    _updateBtn.Enabled = true;
                    break;
            }
        }

        public static VersionType CalcType(String current)
        {
            var a = AssemblyInfo.Instance().AssemblyVersion.Split('.');
            var cv = current.Split('.');
            if (a.Length != cv.Length)
            {
                return VersionType.UNKNOWN;
            }

            var thisV = new int[a.Length];
            var curV = new int[a.Length];
            try
            {

                for (var i = 0; i < a.Length; i++)
                {
                    thisV[i] = int.Parse(a[i].Trim());
                    curV[i] = int.Parse(cv[i].Trim());
                }
            }
            catch
            {
                return VersionType.UNKNOWN;
            }

            var isSame = true;

            for (var i = 0; i < a.Length; i++)
           {
               if (thisV[i] != curV[i])
               {
                   isSame = false;
               }
           }

            if(isSame)
            {
                return VersionType.SAME;
            }

            var isBigger = true;

            for (var i = 0; i < a.Length; i++)
            {
                if (curV[i] != thisV[i])
                {
                    if (curV[i] > thisV[i])
                    {
                        for (var j = 0; j < i; j++)
                        {
                            if (curV[j] < thisV[j])
                            {
                                isBigger = false;
                                goto Return;
                            }
                        }
                    }
                    else
                    {
                        for (var j = 0; j < i; j++)
                        {
                            if (curV[j] < thisV[j])
                            {
                                isBigger = false;
                                goto Return;
                            }
                        }
                       
                        isBigger = false;
                        goto Return;
                    }
                }
            }
            
            Return:
            {
                return isBigger ? VersionType.BIGGER : VersionType.LOWER;
            }
        }

        #endregion

        #region Progress Bar's

        public void UpdateProgressBar(int persent, bool total)
        {
            var d = new DelegateCall(this, new MainForm.UpdateProgressBarDelegate(updateProgressBarUnsafe), persent,
                                     total);
            Invoke(d);
        }

        private void updateProgressBarUnsafe(int pe, bool total)
        {
            if (total)
            {

                if (pe == 0)
                {
                    Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
                }
                else
                {
                    Windows7Taskbar.SetProgressValue(Handle, pe, 100);
                }

                _totalProgress.Value = pe;
                _totalProgress.Refresh();
            }
            else
            {
                _fileProgressBar.Value = pe;
                _fileProgressBar.Refresh();
            }
        }

        #endregion

        #region Invoke Helpers
        private void Invoke(DelegateCall a)
        {
            if (!IsDisposed)
            {
                InvokeManager.AddInvoke(a);
            }
        }

        #endregion

        #region ListLoader
        public GUListLoaderTask ListLoader
        {
            get
            {
                return _listLoaderTask;
            }
        }
        #endregion
    }
}