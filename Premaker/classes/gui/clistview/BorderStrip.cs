using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace com.jds.Premaker.classes.gui.clistview
{
    /// <summary>
    ///   Summary description for BorderStrip.
    /// </summary>
    internal class BorderStrip : Control
    {
        #region BorderTypes enum

        public enum BorderTypes
        {
            btLeft = 0,
            btRight = 1,
            btTop = 2,
            btBottom = 3,
            btSquare = 4
        } ;

        #endregion

        /// <summary>
        ///   Required designer variable.
        /// </summary>
        private Container components;

        public BorderStrip()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitComponent call
        }

        /// <summary>
        ///   how the control looks on the outside
        /// </summary>
        public BorderTypes BorderType { get; set; }

        /// <summary>
        ///   Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            switch (BorderType)
            {
                case BorderTypes.btSquare:
                    {
                        ControlPaint.DrawBorder3D(pe.Graphics, ClientRectangle, Border3DStyle.SunkenInner);
                        // draw control border			
                        //pe.Graphics.FillRectangle( SystemBrushes.Control, this.ClientRectangle );
                        break;
                    }

                case BorderTypes.btLeft:
                    {
                        // NOTE, the reason to make a fake rect is because we are specifically looking for only a part of the rect which in this case is the left 2
                        // however, we have to make it bigger for it to draw an entire left side with 2 pixels, i made it 8 just for safety
                        Rectangle tmpRect = new Rectangle(0, 0, 8, ClientRectangle.Height);
                        ControlPaint.DrawBorder3D(pe.Graphics, tmpRect, Border3DStyle.Sunken); // draw control border			
                        break;
                    }

                case BorderTypes.btRight:
                    {
                        // this should put only the right 2 pixels of the border on the visible strip (i hope)
                        Rectangle tmpRect = new Rectangle(-6, 0, 8, ClientRectangle.Height);
                        ControlPaint.DrawBorder3D(pe.Graphics, tmpRect, Border3DStyle.Sunken); // draw control border			
                        break;
                    }

                case BorderTypes.btBottom:
                    {
                        Rectangle tmpRect = new Rectangle(0, -6, ClientRectangle.Width, 8);
                        ControlPaint.DrawBorder3D(pe.Graphics, tmpRect, Border3DStyle.Sunken); // draw control border			
                        break;
                    }

                case BorderTypes.btTop:
                    {
                        Rectangle tmpRect = new Rectangle(0, 0, ClientRectangle.Width, 8);
                        ControlPaint.DrawBorder3D(pe.Graphics, tmpRect, Border3DStyle.Sunken); // draw control border			
                        break;
                    }
            }

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        #region Component Designer generated code

        /// <summary>
        ///   Required method for Designer support - do not modify 
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
    }
}