#region Usage

using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using com.jds.AWLauncher.classes.config;
using com.jds.AWLauncher.classes.events;
using com.jds.AWLauncher.classes.games;
using com.jds.AWLauncher.classes.games.attributes;
using com.jds.AWLauncher.classes.games.propertyes;
using com.jds.AWLauncher.classes.gui;
using com.jds.AWLauncher.classes.gui.tabpane;
using com.jds.AWLauncher.classes.images;
using com.jds.AWLauncher.classes.invoke;
using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.language.enums;
using com.jds.AWLauncher.classes.listloader.enums;
using com.jds.AWLauncher.classes.task_manager;
using com.jds.AWLauncher.classes.task_manager.tasks;
using com.jds.AWLauncher.classes.version_control;
using com.jds.AWLauncher.classes.version_control.gui;
using com.jds.AWLauncher.classes.windows;
using com.jds.AWLauncher.classes.windows.windows7;

#endregion

namespace com.jds.AWLauncher.classes.forms
{
    public sealed partial class MainForm : Form
    {
        #region Nested type: CloseDelegate

        private delegate void CloseDelegate(int timeout);

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
            //SetStyle(ControlStyles.UserPaint, false);
            SetStyle(ControlStyles.FixedWidth, true);

            ChangeLanguage(true);
                                 
            Opacity = 0F;
            _startButton.Enabled = false;
            _fullCheck.Enabled = false;

            SetVersionTypeUnsafe("0.0.0.0", VersionType.UNKNOWN);

            Shown += MainForm_Shown;
            _closeBtn.Click += _closeBtn_Click;
            MouseDown += JPanelTab_MouseDown;
            MouseUp += JPanelTab_MouseUp;
            MouseMove += JPanelTab_MouseMove;
            _tabbedPane.ChangeSelectedTabEvent += jTabbedPane1_ChangeSelectedTabEvent;

            EventHandlers.Register(_homePage);
            EventHandlers.Register(_versionInfo);
            EventHandlers.Register(_faqLabel);
            EventHandlers.Register(_forumLabel);
            EventHandlers.Register(_joinNowLabel);
            EventHandlers.Register(_rulesLabel);

            //добавляем все игры в вкладки);
            foreach (object enu in Enum.GetValues(typeof (Game)))
            {
                var game = (Game) enu;
                GameProperty prop = RConfig.Instance.getGameProperty(game);
                var pane =
                    (JPanelTab)
                    ((EnumPane)
                     game.GetType().GetField(game.ToString()).GetCustomAttributes(typeof (EnumPane), false).GetValue(0))
                        .Type.InvokeMember(null, BindingFlags.CreateInstance, null, null, new Object[] {prop});

                _tabbedPane.addTab(pane);

                if (game == RConfig.Instance.ActiveGame)
                {
                    _tabbedPane.SelectedTab = pane;
                }
            }
        }

        const int CS_VREDRAW = 1;
        const int CS_HREDRAW = 2;

     /**   protected override void WndProc(ref Message m)
        {
            WM_MESSAGE wm = (WM_MESSAGE) m.Msg;
            Console.Write(wm);
            switch(wm)
            {
                case WM_MESSAGE.WM_ONE:
                case WM_MESSAGE.WM_PAINT:
                case WM_MESSAGE.WM_MOUSEMOVE:
                case WM_MESSAGE.WM_NCCREATE:
                case WM_MESSAGE.WM_SETICON:
                case WM_MESSAGE.WM_STYLECHANGING:
                case WM_MESSAGE.WM_STYLECHANGED:
                case WM_MESSAGE.WM_SHOWWINDOW:
                case WM_MESSAGE.WM_ACTIVATE:
                case WM_MESSAGE.WM_SYSCOMMAND:
                case WM_MESSAGE.WM_WINDOWPOSCHANGED:
                case WM_MESSAGE.WM_SIZE:
                case WM_MESSAGE.WM_CLOSE:
                case WM_MESSAGE.WM_DESTROY:
                case WM_MESSAGE.WM_DRAWITEM:
                case WM_MESSAGE.WM_PARENTNOTIFY:
                case WM_MESSAGE.WM_MOVE:
                case WM_MESSAGE.WM_ERASEBKGND:
                case WM_MESSAGE.WM_NCHITTEST:
                case WM_MESSAGE.WM_WINDOWPOSCHANGING:
                case WM_MESSAGE.WM_ACTIVATEAPP:
                case WM_MESSAGE.WM_NCACTIVATE:
                case WM_MESSAGE.WM_SETFOCUS:
                case WM_MESSAGE.WM_NCPAINT:
                case WM_MESSAGE.WM_QUERYOPEN:
                case WM_MESSAGE.WM_GETICON:
                case WM_MESSAGE.WM_IME_SETCONTEXT:
                case WM_MESSAGE.WM_IME_NOTIFY:
                case WM_MESSAGE.WM_KILLFOCUS:
                case WM_MESSAGE.WM_GETTEXT:
                case WM_MESSAGE.WM_GETTEXTLENGTH:
                case WM_MESSAGE.WM_NCCALCSIZE:
                case 0xc323:
                    Console.WriteLine(" ----jjj-");
                    base.WndProc(ref m);
                    return;
            }
            Console.WriteLine();
        }        */
       
        public WM_MESSAGE valueOf(int id)
        {
            return (WM_MESSAGE) id;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.Style |= (int) WindowStyles.MinimizeBox;
                cp.ClassStyle = CS_VREDRAW | CS_HREDRAW;

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

            ShowAllItems(false, false, true);

            CheckInstalled(true);

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
            InvokeManager.Instance.Shutdown = true;


            RConfig.Instance.X = Location.X;
            RConfig.Instance.Y = Location.Y;

            RConfig.Instance.save();

            foreach (JPanelTab pane in _tabbedPane.Values)
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
                _tabbedPane.Visible = visible;
                _faqLabel.Visible = visible;
                _forumLabel.Visible = visible;
                _joinNowLabel.Visible = visible;
                _settingsButton.Visible = visible;
                _minimizedButton.Visible = visible;
                _closeBtn.Visible = visible;
                _rulesLabel.Visible = visible;
                _separator1.Visible = visible;
                _separator2.Visible = visible;
                _separator3.Visible = visible;
                _separator4.Visible = visible;
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

        public void CloseWithout(int timeout)
        {
            _notTransperty = true;

            CloseS(timeout);
        }

        private void Form_SizeChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("t");
        }

        public void UpdateAllRSS()
        {
            foreach (JPanelTab pane in _tabbedPane.Values)
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
            return _tabbedPane.SelectedTab == p;
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
            if (!_tabbedPane.IsSelectionDisabled)
                PropertyForm.Instance().ShowDialog(this);
        }

        private void _closeBtn_Click(object sender, EventArgs e)
        {
            CloseS(0);
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

        public void UpdateStatusLabel(WordEnum wordEnum, params Object[] arg)
        {
            UpdateStatusLabel(LanguageHolder.Instance()[wordEnum], arg);
        }

        public void UpdateStatusLabel(String a, params Object[] arg)
        {
            Invoke(new DelegateCall(this, new UpdateStatusLabelDelegate(UpdateStatusLabelUnsafe), String.Format(a, arg)));
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
            if(pe < 0)
            {
                pe = 0;
            }

            if(pe > 100)
            {
                pe = 100;
            }

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
                //_totalProgress.Refresh();
            }
            else
            {
                _fileProgressBar.Value = pe;
               //_fileProgressBar.Refresh();
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
            if (Visible)
            {
                Invoke(new DelegateCall(this, new SetFormStateDelegate(SetMainFormUnsafe), type));
            }
            else
            {
                FormState = type;
            }
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
                    _fullCheck.Enabled = true;
                    _startButton.Enabled = true;
                    break;
                case MainFormState.CHECKING:
                    _fullCheck.Info = ImageHolder.Instance()[PictureName.CANCEL];
                    _startButton.Enabled = false;
                    _fullCheck.Enabled = true;
                    break;
            }

            _tabbedPane.IsSelectionDisabled = (s != MainFormState.NONE);
            FormState = s;
        }

        #endregion

        #region Close Form

        public void CloseS(int timeout)
        {
            Invoke(new DelegateCall(this, new CloseDelegate(CloseUnsafe), timeout));
        }

        private void CloseUnsafe(int timeout)
        {
            if (timeout > 0)
            {
                Visible = false;
                Thread.Sleep(timeout);
            }

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
                _startButton.Enabled = can;
                _fullCheck.Enabled = can;
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
                InvokeManager.Instance.AddInvoke(delegateCall);
            }
        }
        #endregion
    }
}