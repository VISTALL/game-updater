using System;
using System.Drawing;
using System.Windows.Forms;

namespace com.jds.GUpdater.classes.gui.tabpane
{
    public class JImageTab : PictureBox
    {
        private bool _pressed;

        public JImageTab()
        {
            MouseEnter += JImageTab_MouseEnter;
            MouseLeave += JImageTab_MouseLeave;
            MouseDown += JImageTab_MouseDown;
            MouseUp += JImageTab_MouseUp;
        }

        #region Events 

        private void JImageTab_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void JImageTab_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Pressed)
                if (PressedImage != null)
                {
                    Image = PressedImage;
                }
        }

        private void JImageTab_MouseLeave(object sender, EventArgs e)
        {
            if (NormalImage != null)
            {
                Image = !_pressed ? NormalImage : ActiveImage;
            }
        }

        private void JImageTab_MouseEnter(object sender, EventArgs e)
        {
            if (!Pressed)
                if (EnterImage != null)
                {
                    Image = EnterImage;
                }
        }

        #endregion

        #region Propertyes

        public bool Pressed
        {
            get { return _pressed; }
            set
            {
                Image = value ? ActiveImage : NormalImage;
                _pressed = value;
            }
        }

        public Image NormalImage { get; set; }

        public Image EnterImage { set; get; }

        public Image PressedImage { get; set; }
        public Image ActiveImage { get; set; }

        #endregion
    }
}