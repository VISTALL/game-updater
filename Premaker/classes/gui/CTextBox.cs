using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using com.jds.Premaker.classes.gui.clistview;

namespace com.jds.Premaker.classes.gui
{
    /// <summary>
    ///   Summary description for CTextBox.
    /// </summary>
    internal class CTextBox : TextBox, CEmbeddedControl
    {
        /// <summary>
        ///   Required designer variable.
        /// </summary>
        private Container components;

        protected CListView m_Parent;
        protected CListItem m_item;
        protected CListSubItem m_subItem;

        public CTextBox()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitComponent call
        }

        #region GLEmbeddedControl Members

        public CListItem Item
        {
            get { return m_item; }
            set { m_item = value; }
        }

        public CListSubItem SubItem
        {
            get { return m_subItem; }
            set { m_subItem = value; }
        }

        public CListView ListControl
        {
            get { return m_Parent; }
            set { m_Parent = value; }
        }


        public string GLReturnText()
        {
            return Text;
        }


        public bool GLLoad(CListItem item, CListSubItem subItem, CListView listctrl)
        // populate this control however you wish with item
        {
            // set the styles you want for this
            BorderStyle = BorderStyle.None;
            AutoSize = false;


            m_item = item;
            m_subItem = subItem;
            m_Parent = listctrl;

            Text = subItem.Text;

            return true; // we don't do any heavy processing in this ctrl so we just return true
        }

        public void GLUnload() // take information from control and return it to the item
        {
            m_subItem.Text = Text;
        }

        #endregion

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
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            Debug.WriteLine("Got Focus");

            base.OnGotFocus(e);
        }


        protected override void OnLostFocus(EventArgs e)
        {
            Debug.WriteLine("Lost Focus");

            base.OnLostFocus(e);
        }

        private void GLTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("keypress edit control");
        }

        #region Component Designer generated code

        /// <summary>
        ///   Required method for Designer support - do not modify 
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // CTextBox
            // 
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GLTextBox_KeyPress);
        }

        #endregion
    }
}