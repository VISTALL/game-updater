using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using com.jds.AWLauncher.classes.forms;
using com.jds.AWLauncher.classes.games.propertyes;

namespace com.jds.AWLauncher.classes.games
{
    public class GameDirectorySelect : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            PropertyForm.Instance().FolderDialog.SelectedPath = value.ToString();
            var p = (GameProperty) context.Instance;
            if (PropertyForm.Instance().FolderDialog.ShowDialog() == DialogResult.OK)
            {
                PropertyForm.Instance().FolderDialog.Dispose();
                String v = PropertyForm.Instance().FolderDialog.SelectedPath;
                p.Path = v;
                return v;
            }

            return value;
        }
    }
}