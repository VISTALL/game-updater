using System;
using System.Drawing;
using System.Windows.Forms;

namespace com.jds.AWLauncher.classes.gui
{
    public class VButton : UserControl
    {
        private IImageInfo _info;

        public VButton()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            MouseEnter += VButton_MouseEnter;
            MouseDown += VButton_MouseDown;
            MouseLeave += VButton_MouseLeave;
            MouseUp += VButton_MouseUp;
            EnabledChanged += VButtonEnabled;
        }

        public Image CurrentImage
        {
            get;
            set;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            if (CurrentImage == null)
            {
                e.Graphics.FillRectangle(Brushes.White, e.ClipRectangle);
                return;
            }

            e.Graphics.DrawImage(CurrentImage, e.ClipRectangle);
        }

        public IImageInfo Info
        {
            get { return _info; }
            set
            {
                _info = value;

                if (value != null)
                {
                    CurrentImage = value.NormalImage();
                    Width = CurrentImage.Width;
                    Height = CurrentImage.Height;
                    Invalidate();
                }
            }
        }
        private void VButtonEnabled(object sender, EventArgs eventArgs)
        {
            if (Info == null)
                return;

            if (Enabled)
            {
                if (Info.NormalImage() != null)
                {
                    CurrentImage = Info.NormalImage();
                    Invalidate();
                }
            }
            else
            {
                if (Info.DisableImage() != null)
                {
                    CurrentImage = Info.DisableImage();
                    Invalidate();
                }
            }
        }

        private void VButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (!Enabled)
            {
                return;
            }
            if (Info != null)
            {
                CurrentImage = Info.EnterImage();
                Invalidate();
            }
        }

        private void VButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Enabled)
            {
                return;
            }
            if (Info != null)
            {
                CurrentImage = Info.PressedImage();
                Invalidate();
            }
        }

        private void VButton_MouseEnter(object sender, EventArgs e)
        {
            if (!Enabled)
            {
                return;
            }
            if (Info != null)
            {
                CurrentImage = Info.EnterImage();
                Invalidate();
            }
        }

        private void VButton_MouseLeave(object sender, EventArgs e)
        {
            if (!Enabled)
            {
                return;
            }
            if (Info != null)
            {
                CurrentImage = Info.NormalImage();
                Invalidate();
            }
        }
    }
}