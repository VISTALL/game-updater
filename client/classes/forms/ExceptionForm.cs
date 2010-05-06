using System;
using System.Windows.Forms;
using com.jds.AWLauncher.classes.version_control;

namespace com.jds.AWLauncher.classes.forms
{
    public partial class ExceptionForm : Form
    {
        public ExceptionForm(Exception e)
        {
            InitializeComponent();

            var text = "";
            try
            {
                text += "Version: " + AssemblyInfo.Instance().AssemblyVersion + "\n";
            }
            catch (Exception)
            {
                text += "Version: NONE\n";
            }
            text += "Exception Name: " + e.GetType().FullName  + "\n";
            text += "Message: " + e.Message + "\n";
            text += "Called Method: " + e.TargetSite.Name + "\n";
            text += "Called Class: " + e.TargetSite.ReflectedType.FullName + "\n";
            text += "Trace: \n" + e.StackTrace;


            _exceptionText.Text = text;

            Application.Run(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
