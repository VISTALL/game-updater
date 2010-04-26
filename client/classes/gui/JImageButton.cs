using System;
using System.Drawing;
using System.Windows.Forms;

namespace com.jds.AWLauncher.classes.gui
{
    public class JImageButton : PictureBox
    {
        private bool _enalble;
        private IImageInfo _info;

        public JImageButton()
        {
            MouseEnter += JImageButton_MouseEnter;
            MouseDown += JImageButton_MouseDown;
            MouseLeave += JImageButton_MouseLeave;
            MouseUp += JImageButton_MouseUp;

            BackColor = Color.Black;

            _enalble = true;
        }


        public IImageInfo Info
        {
            get { return _info; }
            set
            {
                _info = value;

                if (value != null)
                {
                    Image = value.NormalImage();
                }

                Refresh();
            }
        }

        public bool Enable
        {
            get { return _enalble; }
            set
            {
                _enalble = value;
                Enabled = value;

                if (Info == null)
                    return;

                if (_enalble)
                {
                    if (Info.NormalImage() != null)
                    {
                        Image = Info.NormalImage();
                    }
                }
                else
                {
                    if (Info.DisableImage() != null)
                    {
                        Image = Info.DisableImage();
                    }
                }
            }
        }

        private void JImageButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_enalble)
            {
                return;
            }
            if (Info != null)
            {
                Image = Info.EnterImage();
            }
        }

        private void JImageButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_enalble)
            {
                return;
            }
            if (Info != null)
            {
                Image = Info.PressedImage();
            }
        }

        private void JImageButton_MouseEnter(object sender, EventArgs e)
        {
            if (!_enalble)
            {
                return;
            }
            if (Info != null)
            {
                Image = Info.EnterImage();
            }
        }

        private void JImageButton_MouseLeave(object sender, EventArgs e)
        {
            if (!_enalble)
            {
                return;
            }
            if (Info != null)
            {
                Image = Info.NormalImage();
            }
        }
    }
}