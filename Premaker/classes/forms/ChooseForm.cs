using System.Windows.Forms;

namespace com.jds.Premaker.classes.forms
{
    public partial class ChooseForm : Form
    {
        public ChooseForm()
        {
            InitializeComponent();
        }

        private void _goButton_Click(object sender, System.EventArgs e)
        {
            Form f = null;
            if(_gameFiles.Checked)
            {
                f = new GFilesForm();
            }
            else
            {
                f = new GUFilesForm();
            }
            f.ShowDialog(this);
        }
    }
}
