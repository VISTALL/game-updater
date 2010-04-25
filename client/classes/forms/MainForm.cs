#region Usage

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using com.jds.GUpdater.classes.config;
using com.jds.GUpdater.classes.events;
using com.jds.GUpdater.classes.games;
using com.jds.GUpdater.classes.games.attributes;
using com.jds.GUpdater.classes.games.propertyes;
using com.jds.GUpdater.classes.gui;
using com.jds.GUpdater.classes.gui.tabpane;
using com.jds.GUpdater.classes.images;
using com.jds.GUpdater.classes.invoke;
using com.jds.GUpdater.classes.language;
using com.jds.GUpdater.classes.language.enums;
using com.jds.GUpdater.classes.listloader.enums;
using com.jds.GUpdater.classes.task_manager;
using com.jds.GUpdater.classes.task_manager.tasks;
using com.jds.GUpdater.classes.version_control;
using com.jds.GUpdater.classes.version_control.gui;
using com.jds.GUpdater.classes.windows;
using com.jds.GUpdater.classes.windows.windows7;
using com.jds.GUpdater.classes.utils;
#endregion

namespace com.jds.GUpdater.classes.forms
{
    public sealed partial class MainForm : Form
    {
        private readonly InvokeManager InvokeManager = new InvokeManager(typeof(MainForm));

        #region Nested type: CloseDelegate

        private delegate void CloseDelegate();

        #endregion
      
        #region Nested type: SetFormStateDelegate

        public delegate void SetFormStateDelegate(MainFormState a);

        #endregion

        #region Nested type: SetOpacityDeledate

        public delegate void SetOpacityDeledate(float d);

        #endregion

        #region Nested type: UpdateProgressBarDelegate

        public delegate void UpdateProgressBarDelegate(int persent, bool t);

        #endregion

        #region Nested type: UpdateStatusLabelDelegate

        public delegate void UpdateStatusLabelDelegate(String a);

        #endregion

        #region Nested type: SetVersionTypeDelegate

        public delegate void SetVersionTypeDelegate(String va, VersionType a);

        #endregion

        #region Instance & Consts & Variables

        private static MainForm _instance;

        private bool _notTransperty;

        public static MainForm Instance
        {
            get { return _instance ?? (_instance = new MainForm()); }
        }

        #endregion

        #region Конструктор и главные методы формы

        public MainForm()
        {
            InitializeComponent();
            ChangeLanguage();

            Opacity = 0F;
            SetVersionTypeUnsafe("0.0.0.0", VersionType.UNKNOWN);

            Shown += MainForm_Shown;
            _closeBtn.Click += _closeBtn_Click;
            MouseDown += JPanelTab_MouseDown;
            MouseUp += JPanelTab_MouseUp;
            MouseMove += JPanelTab_MouseMove;
            TabbedPane.ChangeSelectedTabEvent += jTabbedPane1_ChangeSelectedTabEvent;


            EventHandlers.Register(_homePage);
            EventHandlers.Register(_versionInfo);

            //добавляем все игры в вкладки
            foreach (object enu in Enum.GetValues(typeof (Game)))
            {
                var game = (Game) enu;
                GameProperty prop = RConfig.Instance.getGameProperty(game);
                var pane =
                    (JPanelTab)
                    ((EnumPane)
                     game.GetType().GetField(game.ToString()).GetCustomAttributes(typeof (EnumPane), false).GetValue(0))
                        .Type.InvokeMember(null, BindingFlags.CreateInstance, null, null, new Object[] {prop});

                TabbedPane.addTab(pane);

                if (game == RConfig.Instance.ActiveGame)
                {
                    TabbedPane.SelectedTab = pane;
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.Style = (int) WindowStyles.MinimizeBox;
                cp.ClassStyle = 8;

                return cp;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (RConfig.Instance.X > 0 && RConfig.Instance.Y > 0)
            {
                Location = new Point(RConfig.Instance.X, RConfig.Instance.Y);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            ShowAllItems(true, true, false);

            UpdateAllRSS();

            UpdateStatusLabel("");

            CheckInstalled(true);

            ShowAllItems(false, false, true);
           
            if (RConfig.Instance.CheckVersionOnStart)
            {
                TaskManager.Instance.AddTask(AssemblyPage.Instance().ListLoader);
            }

            if (RConfig.Instance.CheckCriticalOnStart)
            {
                GameProperty p = RConfig.Instance.getGameProperty(RConfig.Instance.ActiveGame);

                if (!CheckInstalled(false))
                {
                    return;
                }

                if (!p.ListLoader.IsValid)
                {
                    TaskManager.Instance.AddTask(p.ListLoader);
                }

                TaskManager.Instance.AddTask(new AnalyzerTask(p, ListFileType.CRITICAL));
            }
        }
       
        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (!_notTransperty)
            {
                for (int i = 1; i <= 100; i += 1)
                {
                    setOpacity((100 - i) / 100F);
                    Thread.Sleep(10);
                }
            }

            TaskManager.Instance.Close(true);
            InvokeManager.Shutdown = true;
            AssemblyPage.Instance().InvokeManager.Shutdown = true;


            RConfig.Instance.X = Location.X;
            RConfig.Instance.Y = Location.Y;

            RConfig.Instance.save();

            foreach (JPanelTab pane in TabbedPane.Values)
            {
                if (pane is IGamePanel)
                {
                    ((IGamePanel)pane).close();
                }
            }
        }

        public void ShowAllItems(bool items, bool visible, bool trans)
        {
            if (items)
            {
                _lastNews.Visible = visible;
                _selectGameLabel.Visible = visible;
                _homePage.Visible = visible;
                _startButton.Visible = visible;
                _fullCheck.Visible = visible;
                _fileProgressBar.Visible = visible;
                _totalProgress.Visible = visible;
                _statusLabel.Visible = visible;
                _infoStart.Visible = visible;
                TabbedPane.Visible = visible;
                _faqLabel.Visible = visible;
                _forumLabel.Visible = visible;
                _joinNowLabel.Visible = visible;
                _settingsButton.Visible = visible;
                _minimizedButton.Visible = visible;
                _closeBtn.Visible = visible;
                _separator1.Visible = visible;
                _separator2.Visible = visible;
                _separator3.Visible = visible;
            }

            if (trans)
            {
                for (int i = 1; i <= 100; i += 1)
                {
                    setOpacity(i/100F);
                    Thread.Sleep(10);
                }
                Opacity = 1F;
            }
        }

        public void CloseWithout()
        {
            _notTransperty = true;

            CloseS();
        }

        private void Form_SizeChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("t");
        }

        public void UpdateAllRSS()
        {
            foreach (JPanelTab pane in TabbedPane.Values)
            {
                if (pane is IGamePanel)
                {
                    ((IGamePanel) pane).refreshNews();
                }
            }
        }

        #endregion

        #region Tab's Actions

        public bool isSelected(JPanelTab p)
        {
            return TabbedPane.SelectedTab == p;
        }

        private void jTabbedPane1_ChangeSelectedTabEvent(JTabbedPane parent, JPanelTab tab)
        {
            if (tab is IGamePanel)
            {
                RConfig.Instance.ActiveGame = ((IGamePanel) tab).getGame();
            }

            if (CheckInstalled(true))
            {
                UpdateStatusLabel("");
            }
        }

        #endregion

        #region Button Events

        private void _settingButton_Click(object sender, EventArgs e)
        {
            if (!TabbedPane.IsSelectionDisabled)
                PropertyForm.Instance().ShowDialog(this);
        }

        private void _closeBtn_Click(object sender, EventArgs e)
        {
            CloseS();
        }

        private void _startButton_Click(object sender, EventArgs e)
        {
            if (FormState != MainFormState.NONE)
            {
                return;
            }

            GameProperty p = RConfig.Instance.getGameProperty(RConfig.Instance.ActiveGame);

            if (!CheckInstalled(true))
            {
                return;
            }

            if (!p.ListLoader.IsValid)
            {
                TaskManager.Instance.AddTask(p.ListLoader);
            }

            TaskManager.Instance.AddTask(new AnalyzerTask(p, ListFileType.CRITICAL));
            TaskManager.Instance.AddTask(new GameStartTask(p));
        }

        private void _fullCheck_Click(object sender, EventArgs e)
        {
            switch (FormState)
            {
                case MainFormState.CHECKING:
                    TaskManager.Instance.Close(false);
                    break;
                case MainFormState.NONE:
                    GameProperty p = RConfig.Instance.getGameProperty(RConfig.Instance.ActiveGame);

                    if (!CheckInstalled(true))
                    {
                        return;
                    }

                    if (!p.ListLoader.IsValid)
                    {
                        TaskManager.Instance.AddTask(p.ListLoader);
                    }

                    TaskManager.Instance.AddTask(new AnalyzerTask(p, ListFileType.NORMAL));
                    break;
            }
        }

        private void _minimizedButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region Panel Move

        private Boolean MOUSE_DOWN;
        private Point POSITION;

        private void JPanelTab_MouseDown(object sender, MouseEventArgs e)
        {
            POSITION.X = e.X;
            POSITION.Y = e.Y;
            MOUSE_DOWN = true;
        }

        private void JPanelTab_MouseUp(object sender, MouseEventArgs e)
        {
            MOUSE_DOWN = false;
        }

        private void JPanelTab_MouseMove(object sender, MouseEventArgs e)
        {
            if (MOUSE_DOWN)
            {
                Point current_pos = MousePosition;
                current_pos.X = current_pos.X - POSITION.X;
                current_pos.Y = current_pos.Y - POSITION.Y;
                Instance.Location = current_pos;
            }
        }

        #endregion

        #region Status Label

        public void UpdateStatusLabel(WordEnum wordEnum)
        {
            UpdateStatusLabel(LanguageHolder.Instance()[wordEnum]);
        }

        public void UpdateStatusLabel(String a)
        {
            Invoke(new DelegateCall(this, new UpdateStatusLabelDelegate(UpdateStatusLabelUnsafe), a));
        }

        private void UpdateStatusLabelUnsafe(String s)
        {
            _statusLabel.Text = s;
        }

        #endregion

        #region Version Control

        private static readonly Color COLOR = Color.FromArgb(155, 137, 113);
        private static readonly Color COLOR2= Color.FromArgb(187, 157, 167);

        public void SetVersionType(String av, VersionType a)
        {
            Invoke(new DelegateCall(this, new SetVersionTypeDelegate(SetVersionTypeUnsafe), av, a));
        }

        private void SetVersionTypeUnsafe(String av, VersionType a)
        {
            VersionType = a;
            Version = av;

            switch (a)
            {
                case VersionType.UNKNOWN:
                    _versionInfo.Text = LanguageHolder.Instance()[WordEnum.VERSION_IS_NOT_CHECK];
                    _versionInfo.ForeColor = COLOR2;
                    _versionInfo.Tag = "ASSEMBLY";
                    _versionInfo.Cursor = Cursors.Hand;
                    break;
                case VersionType.SAME:
                case VersionType.LOWER:
                    _versionInfo.Text = LanguageHolder.Instance()[WordEnum.VERSION_IS_OK];
                    _versionInfo.ForeColor = COLOR;
                    _versionInfo.Cursor = Cursors.Default;
                    _versionInfo.Tag = null;
                    break;
                case VersionType.BIGGER:
                    _versionInfo.Text = LanguageHolder.Instance()[WordEnum.VERSION_IS_BAD];
                    _versionInfo.ForeColor = Color.Red;
                    _versionInfo.Tag = "ASSEMBLY";
                    _versionInfo.Cursor = Cursors.Hand;
                    break;
            }
        }

        public String Version
        {
            get; set;
        }

        public VersionType VersionType
        {
            get; set;
        }
        #endregion

        #region Progress Bar's

        public void UpdateProgressBar(int persent, bool total)
        {
            Invoke(new DelegateCall(this, new UpdateProgressBarDelegate(updateProgressBarUnsafe), persent, total));
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

        #region Opacity

        public void setOpacity(float d)
        {
            Invoke( new DelegateCall(this, new SetOpacityDeledate(setOpacityUnsafe), d));
        }

        private void setOpacityUnsafe(float d)
        {
            Opacity = d;
        }

        #endregion

        #region Main Form State

        public MainFormState FormState { get; set; }

        public void SetMainFormState(MainFormState type)
        {
            Invoke(new DelegateCall(this, new SetFormStateDelegate(SetMainFormUnsafe), type));
        }

        /**
         * NONE - ставит фулл  чек кнопку и включает старт кнопку
         * CHECKING - ставит кенсел кнопку, и отключает старт кнопку
         */

        private void SetMainFormUnsafe(MainFormState s)
        {
            switch (s)
            {
                case MainFormState.NONE:
                    _fullCheck.Info = ImageHolder.Instance()[PictureName.FULLCHECK];
                    _fullCheck.Enable = true;
                    _startButton.Enable = true;
                    break;
                case MainFormState.CHECKING:
                    _fullCheck.Info = ImageHolder.Instance()[PictureName.CANCEL];
                    _startButton.Enable = false;
                    _fullCheck.Enable = true;
                    break;
            }

            TabbedPane.IsSelectionDisabled = (s != MainFormState.NONE);
            FormState = s;
        }

        #endregion

        #region Close Form

        public void CloseS()
        {
            if (IsCanInvoke)
            {
                if (InvokeRequired)
                {
                    Invoke(new CloseDelegate(CloseUnsafe));
                }
                else
                {
                    CloseUnsafe();
                }
            }
        }

        private void CloseUnsafe()
        {
            Close();
        }

        #endregion

        #region Helper Methods


        public bool CheckInstalled(bool btm)
        {
            GameProperty p = RConfig.Instance.getGameProperty(RConfig.Instance.ActiveGame);
            bool can = p.Installed && p.isEnable();

            if (!p.isEnable())
            {
                UpdateStatusLabel(WordEnum.GAME_IS_DISABLED);
            }

            else if (!p.Installed)
            {
                UpdateStatusLabel(WordEnum.NOT_INSTALLED);
            }

            if (btm)
            {
                _startButton.Enable = p.Installed && p.isEnable();
                _fullCheck.Enable = p.Installed && p.isEnable();
            }

            if (can)
            {
                UpdateStatusLabel("");
            }

            return can;
        }

        #endregion

        #region Invoke Helpers 
        
        public bool IsCanInvoke
        {
            get { return !IsDisposed && !Disposing; }
        }

        public void Invoke(DelegateCall delegateCall)
        {
            if(IsCanInvoke)
            {
                InvokeManager.AddInvoke(delegateCall);
            }
        }
        #endregion

    }
}