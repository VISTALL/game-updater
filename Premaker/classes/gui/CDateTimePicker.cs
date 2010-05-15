using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using com.jds.Premaker.classes.gui.clistview;

namespace com.jds.Premaker.classes.gui
{
    /// <summary>
    ///   Summary description for GLDateTimePicker.
    /// </summary>
    internal class CDateTimePicker : DateTimePicker, CEmbeddedControl
    {
        /// <summary>
        ///   Required designer variable.
        /// </summary>
        private Container components;

        protected CListView m_Parent;
        protected CListItem m_item;
        protected CListSubItem m_subItem;

        public CDateTimePicker()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        #region CEmbeddedControl Members

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
        {
            Format = DateTimePickerFormat.Long;
            try
            {
                m_item = item;
                m_subItem = subItem;
                m_Parent = listctrl;

                Text = subItem.Text;

                //this.Value = subItem.Text;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());

                Text = DateTime.Now.ToString();
            }

            return true;
        }

        public void GLUnload()
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
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
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