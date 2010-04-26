using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using com.jds.AWLauncher.classes.gui.tabpane;

namespace com.jds.AWLauncher.classes.gui
{
    public partial class JTabbedPane : UserControl
    {
        #region Delegates

        public delegate void ChangeSelectedTab(JTabbedPane parent, JPanelTab tab);

        #endregion

        private readonly Dictionary<int, JPanelTab> _pages = new Dictionary<int, JPanelTab>();
        private JPanelTab _selected;

        public JTabbedPane()
        {
            InitializeComponent();

            IsSelectionDisabled = false;
            //CheckForIllegalCrossThreadCalls = false;
        }

        //MouseEventHandler
        public JPanelTab SelectedTab
        {
            get { return _selected; }
            set
            {
                if (!IsSelectionDisabled)
                {
                    JPanelTab old = _selected;

                    if (value != null)
                    {
                        value.active();
                    }

                    _selected = value;

                    updateSelectedTab(value);

                    if (old != null)
                    {
                        old.deactive();
                    }
                }
            }
        }

        public event ChangeSelectedTab ChangeSelectedTabEvent;

        public void addTab(JPanelTab tab)
        {
            int index = nextIndex();
            int t = getNextTabY(index, tab.getTab().NormalImage().Height);
            if (index == 0)
            {
                tabPage.Size = new Size(tab.getTab().NormalImage().Width, Height);
            }

            var pic = new JImageTab
                          {
                              BackColor = Color.Transparent,
                              Image = tab.getTab().NormalImage(),
                              NormalImage = tab.getTab().NormalImage(),
                              EnterImage = tab.getTab().EnterImage(),
                              PressedImage = tab.getTab().PressedImage(),
                              ActiveImage = tab.getTab().ActiveImage(),
                              Size = tab.getTab().NormalImage().Size
                          };

            pic.BackColor = Color.Transparent;
            pic.Location = new Point(tabPage.Location.X, t);
            tabPage.Controls.Add(pic);

            tab.initialize(pic, this);

            _pages.Add(index, tab);
        }

        public void updateSelectedTab(JPanelTab pan)
        {
            if (ChangeSelectedTabEvent != null)
            {
                ChangeSelectedTabEvent(this, pan);
            }
        }

        public int getNextTabY(int index, int h)
        {
            int y = 139; //start position

            for (int i = 0; i < index; i++)
            {
                y += h + 12; //tab diff
            }
            return y;
        }

        public int nextIndex()
        {
            if (_pages.Count == 0)
                return 0;
            return _pages.Count;
        }

        #region Propertyes

        public Dictionary<int, JPanelTab>.ValueCollection Values
        {
            get { return _pages.Values; }
        }

        public bool IsSelectionDisabled { get; set; }

        #endregion
    }
}