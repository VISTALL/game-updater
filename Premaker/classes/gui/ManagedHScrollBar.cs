using System;
using System.Windows.Forms;

namespace com.jds.Premaker.classes.gui.clistview
{
    internal class ManagedHScrollBar : HScrollBar
    {
        public ManagedHScrollBar()
        {
            TabStop = false;
            GotFocus += ReflectFocus;
        }


        public int mTop
        {
            set
            {
                if (Top != value)
                    Top = value;
            }
        }

        public int mLeft
        {
            set
            {
                if (value != Left)
                    Left = value;
            }
        }

        public int mWidth
        {
            get
            {
                if (Visible != true)
                    return 0;
                else
                    return Width;
            }
            set
            {
                if (Width != value)
                    Width = value;
            }
        }

        public int mHeight
        {
            get
            {
                if (Visible != true)
                    return 0;
                else
                    return Height;
            }
            set
            {
                if (Height != value)
                    Height = value;
            }
        }

        public bool mVisible
        {
            set
            {
                if (Visible != value)
                    Visible = value;
            }
        }

        public int mSmallChange
        {
            set
            {
                if (SmallChange != value)
                    SmallChange = value;
            }
        }

        public int mLargeChange
        {
            set
            {
                if (LargeChange != value)
                    LargeChange = value;
            }
        }

        public int mMaximum
        {
            set
            {
                if (Maximum != value)
                    Maximum = value;
            }
        }

        public void ReflectFocus(object source, EventArgs e)
        {
            Parent.Focus();
        }

        private void InitializeComponent()
        {
            // 
            // ManagedHScrollBar
            // 
        }
    }
}