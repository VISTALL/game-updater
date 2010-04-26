using System;
using System.Drawing;
using System.Windows.Forms;
using com.jds.AWLauncher.classes.config;
using com.jds.AWLauncher.classes.config.gui;
using com.jds.AWLauncher.classes.games;
using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.language.enums;
using com.jds.AWLauncher.classes.listloader.enums;
using com.jds.AWLauncher.classes.version_control.gui;

namespace com.jds.AWLauncher.classes.forms
{
    public partial class PropertyForm : Form
    {
        private static PropertyForm _instance;

        private readonly TabPage _generalPage;
        private readonly TabPage _versionControlPage;

        #region Instance

        public static PropertyForm Instance()
        {
           return _instance ?? (_instance = new PropertyForm()); 
        }

        #endregion

        #region Конструктор и главные методы формы

        public PropertyForm()
        {
            InitializeComponent();

            //1 вкладка главные настройки
            _generalPage = new TabPage(LanguageHolder.Instance()[WordEnum.GENERAL]);
            var ppage = new PropertyPage(RConfig.Instance) { Location = new Point(3, 3) };
            _generalPage.Controls.Add(ppage);

            _tabs.TabPages.Add(_generalPage);

            foreach (var enu in Enum.GetValues(typeof (Game)))
            {
                var gpage = new TabPage(GameInfo.getNameOf(enu));
                var gp = RConfig.Instance.getGameProperty((Game)enu);
                var gppage = new PropertyPage(gp) {Location = new Point(3, 3), Enabled = gp.isEnable()};


                gpage.Controls.Add(gppage);

                _tabs.TabPages.Add(gpage);
            }

            _versionControlPage = new TabPage(LanguageHolder.Instance()[WordEnum.VERSION_CONTROL]);
            var assemblyPage = AssemblyPage.Instance();
            assemblyPage.Location = new Point(3, 3);
            _versionControlPage.Controls.Add(assemblyPage);

            _tabs.TabPages.Add(_versionControlPage);

            Shown += PropertyForm_Shown;            
        }

        private void PropertyForm_Load(object sender, EventArgs e)
        {
            ChangeLanguage();
        }

        static void PropertyForm_Shown(object sender, EventArgs e)
        {
            AssemblyPage.Instance().SetVersionTypeU(MainForm.Instance.Version, MainForm.Instance.VersionType);
        }

        private void PropertyForm_Closing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance.ChangeLanguage();
            if (MainForm.Instance.FormState == MainFormState.NONE)
            {
                MainForm.Instance.CheckInstalled(true);
            }
        }

        #endregion

        #region Buttons

        private void _okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}