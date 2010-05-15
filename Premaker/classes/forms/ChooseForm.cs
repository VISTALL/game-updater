using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using com.jds.Premaker.classes.windows;

namespace com.jds.Premaker.classes.forms
{
    public partial class ChooseForm : Form
    {
        private static ChooseForm _instance;
        private Form _chooseForm;

        public  static ChooseForm Instance
        {
            get
            {
                return _instance ?? (_instance = new ChooseForm());
            }
        }

        public ChooseForm()
        {
            InitializeComponent();
        }

        private void _goButton_Click(object sender, EventArgs e)
        {
            if(_gameFiles.Checked)
            {
                _chooseForm = new GameFilesForm();
            }
            else
            {
                _chooseForm = new AWFilesForm();
            }

            Visible = false;
            _chooseForm.ShowDialog(this);
            Close();
        }

        public GameFilesForm FormAsGameFiles
        {
            get
            {
                return (GameFilesForm) _chooseForm;
            }
        }
    }
}
