using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace com.jds.AWLauncher.classes.gui
{
    [Description("Color Progress Bar")]
    [ToolboxBitmap(typeof (ProgressBar))]
    [Designer(typeof (ColorProgressBarDesigner))]
    public class ColorProgressBar : Control
    {
        //
        // set default values
        //

        #region FillStyles enum

        public enum FillStyles
        {
            Solid,
            Dashed
        }

        #endregion

        private Color _BarColor = Color.FromArgb(255, 128, 128);
        private Color _BorderColor = Color.Black;
        private FillStyles _FillStyle = FillStyles.Solid;
        private int _Maximum = 100;
        private int _Minimum;
        private int _Step = 10;
        private int _Value;

        public ColorProgressBar()
        {
            base.Size = new Size(150, 15);
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer,
                true
                );
        }

        [Description("ColorProgressBar color")]
        [Category("ColorProgressBar")]
        public Color BarColor
        {
            get { return _BarColor; }
            set
            {
                _BarColor = value;
                Invalidate();
            }
        }

        [Description("ColorProgressBar fill style")]
        [Category("ColorProgressBar")]
        public FillStyles FillStyle
        {
            get { return _FillStyle; }
            set
            {
                _FillStyle = value;
                Invalidate();
            }
        }

        [Description("The current value for the ColorProgressBar, " +
                     "in the range specified by the Minimum and Maximum properties.")]
        [Category("ColorProgressBar")]
        // the rest of the Properties windows must be updated when this peroperty is changed.
        [RefreshProperties(RefreshProperties.All)]
        public int Value
        {
            get { return _Value; }
            set
            {
                if (value < _Minimum)
                {
                    throw new ArgumentException("'" + value + "' is not a valid value for 'Value'.\n" +
                                                "'Value' must be between 'Minimum' and 'Maximum'.");
                }

                if (value > _Maximum)
                {
                    throw new ArgumentException("'" + value + "' is not a valid value for 'Value'.\n" +
                                                "'Value' must be between 'Minimum' and 'Maximum'.");
                }

                _Value = value;
                Invalidate();
            }
        }

        [Description("The lower bound of the range this ColorProgressbar is working with.")]
        [Category("ColorProgressBar")]
        [RefreshProperties(RefreshProperties.All)]
        public int Minimum
        {
            get { return _Minimum; }
            set
            {
                _Minimum = value;

                if (_Minimum > _Maximum)
                    _Maximum = _Minimum;
                if (_Minimum > _Value)
                    _Value = _Minimum;

                Invalidate();
            }
        }

        [Description("The uppper bound of the range this ColorProgressbar is working with.")]
        [Category("ColorProgressBar")]
        [RefreshProperties(RefreshProperties.All)]
        public int Maximum
        {
            get { return _Maximum; }
            set
            {
                _Maximum = value;

                if (_Maximum < _Value)
                    _Value = _Maximum;
                if (_Maximum < _Minimum)
                    _Minimum = _Maximum;

                Invalidate();
            }
        }

        [Description("The amount to jump the current value of the control by when the Step() method is called.")]
        [Category("ColorProgressBar")]
        public int Step
        {
            get { return _Step; }
            set
            {
                _Step = value;
                Invalidate();
            }
        }

        [Description("The border color of ColorProgressBar")]
        [Category("ColorProgressBar")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        //
        // Call the PerformStep() method to increase the value displayed by the amount set in the Step property
        //
        public void PerformStep()
        {
            if (_Value < _Maximum)
                _Value += _Step;
            else
                _Value = _Maximum;

            Invalidate();
        }

        //
        // Call the PerformStepBack() method to decrease the value displayed by the amount set in the Step property
        //
        public void PerformStepBack()
        {
            if (_Value > _Minimum)
                _Value -= _Step;
            else
                _Value = _Minimum;

            Invalidate();
        }

        //
        // Call the Increment() method to increase the value displayed by an integer you specify
        // 
        public void Increment(int value)
        {
            if (_Value < _Maximum)
                _Value += value;
            else
                _Value = _Maximum;

            Invalidate();
        }

        //
        // Call the Decrement() method to decrease the value displayed by an integer you specify
        // 
        public void Decrement(int value)
        {
            if (_Value > _Minimum)
                _Value -= value;
            else
                _Value = _Minimum;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //
            // Calculate matching colors
            //
            Color darkColor = ControlPaint.Dark(_BarColor);
            Color bgColor = ControlPaint.Dark(_BarColor);

            //
            // Fill background
            //
            var bgBrush = new SolidBrush(bgColor);
            e.Graphics.FillRectangle(bgBrush, ClientRectangle);
            bgBrush.Dispose();

            // 
            // Check for value
            //
            if (_Maximum == _Minimum || _Value == 0)
            {
                // Draw border only and exit;
                drawBorder(e.Graphics);
                return;
            }

            //
            // The following is the width of the bar. This will vary with each value.
            //
            int fillWidth = (Width*_Value)/(_Maximum - _Minimum);

            //
            // GDI+ doesn't like rectangles 0px wide or high
            //
            if (fillWidth == 0)
            {
                // Draw border only and exti;
                drawBorder(e.Graphics);
                return;
            }

            //
            // Rectangles for upper and lower half of bar
            //
            var topRect = new Rectangle(0, 0, fillWidth, Height/2);
            var buttomRect = new Rectangle(0, Height/2, fillWidth, Height/2);

            //
            // The gradient brush
            //
            LinearGradientBrush brush;

            //
            // Paint upper half
            //
            brush = new LinearGradientBrush(new Point(0, 0),
                                            new Point(0, Height/2), darkColor, _BarColor);
            e.Graphics.FillRectangle(brush, topRect);
            brush.Dispose();

            //
            // Paint lower half
            // (this.Height/2 - 1 because there would be a dark line in the middle of the bar)
            //
            brush = new LinearGradientBrush(new Point(0, Height/2 - 1),
                                            new Point(0, Height), _BarColor, darkColor);
            e.Graphics.FillRectangle(brush, buttomRect);
            brush.Dispose();

            //
            // Calculate separator's setting
            //
            var sepWidth = (int) (Height*.67);
            int sepCount = (fillWidth/sepWidth);
            Color sepColor = ControlPaint.LightLight(_BarColor);

            //
            // Paint separators
            //
            switch (_FillStyle)
            {
                case FillStyles.Dashed:
                    // Draw each separator line
                    for (int i = 1; i <= sepCount; i++)
                    {
                        e.Graphics.DrawLine(new Pen(sepColor, 1),
                                            sepWidth*i, 0, sepWidth*i, Height);
                    }
                    break;

                case FillStyles.Solid:
                    // Draw nothing
                    break;

                default:
                    break;
            }

            //
            // Draw border and exit
            //
            drawBorder(e.Graphics);
        }

        //
        // Draw border
        //
        protected void drawBorder(Graphics g)
        {
            var borderRect = new Rectangle(0, 0,
                                           ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            g.DrawRectangle(new Pen(_BorderColor, 1), borderRect);
        }
    }

    internal class ColorProgressBarDesigner : ControlDesigner
    {
        // clean up some unnecessary properties
        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("AllowDrop");
            Properties.Remove("BackgroundImage");
            Properties.Remove("ContextMenu");
            Properties.Remove("FlatStyle");
            Properties.Remove("Image");
            Properties.Remove("ImageAlign");
            Properties.Remove("ImageIndex");
            Properties.Remove("ImageList");
            Properties.Remove("Text");
            Properties.Remove("TextAlign");
        }
    }
}