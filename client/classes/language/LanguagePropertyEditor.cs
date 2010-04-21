using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using com.jds.GUpdater.classes.config;
using com.jds.GUpdater.classes.forms;

namespace com.jds.GUpdater.classes.language
{
    public class LanguagePropertyEditor : UITypeEditor
    {
        private IWindowsFormsEditorService fes;

        public override bool IsDropDownResizable
        {
            get { return false; }
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            fes = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
            if (fes == null)
            {
                return value;
            }

            var lb = new ListBox {Sorted = true, SelectionMode = SelectionMode.One, HorizontalScrollbar = true};

            foreach (string lang in LanguageHolder.Instance().Languages)
            {
                lb.Items.Add(lang);
            }

            int index = lb.Items.IndexOf(RConfig.Instance.Language);

            lb.SelectedIndex = index == -1 ? 0 : index;

            if (value != null)
            {
                index = lb.Items.IndexOf(value);
                if (index > 0)
                    lb.SelectedIndex = index;
            }

            lb.SelectedIndexChanged += lb_SelectedIndexChanged;
            fes.DropDownControl(lb);

            

            return lb.SelectedItem.ToString();
        }

        private void lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            fes.CloseDropDown();
        }
    }
}