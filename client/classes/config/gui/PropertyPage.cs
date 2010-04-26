using System;
using System.Windows.Forms;

namespace com.jds.AWLauncher.classes.config.gui
{
    public partial class PropertyPage : UserControl
    {
        public PropertyPage(Object obj)
        {
            InitializeComponent();

            _property.SelectedObject = obj;
        }
    }
}