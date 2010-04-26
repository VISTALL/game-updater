using System.Drawing;
using System.Windows.Forms;

namespace com.jds.AWLauncher.classes.gui.tabpane
{
    public class JPanelTab : UserControl
    {
        private int _imageWidth;
        private JTabbedPane _parent;
        private JImageTab _tab;

        public virtual IImageInfo getTab()
        {
            return null;
        }

        public void initialize(JImageTab tab, JTabbedPane pane)
        {
            _tab = tab;
            _parent = pane;
            _imageWidth = tab.Width;

            tab.MouseClick += tab_MouseClick;
            Location = new Point(tab.Width - tab.Location.X, pane.Location.Y);
            Size = new Size(_parent.Size.Width - tab.Width, _parent.Height);
        }

        private void tab_MouseClick(object sender, MouseEventArgs e)
        {
            if (!_tab.Pressed)
            {
                _parent.SelectedTab = this;
            }
        }

        public void active()
        {
            _tab.Pressed = true;
            _parent.Controls.Add(this);
        }

        public void deactive()
        {
            _tab.Pressed = false;
            _parent.Controls.Remove(this);
        }
    }
}