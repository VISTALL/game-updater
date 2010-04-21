using System;
using System.Drawing;
using System.Windows.Forms;
using com.jds.GUpdater.classes.config;
using com.jds.GUpdater.classes.config.gui;
using com.jds.GUpdater.classes.games;
using com.jds.GUpdater.classes.games.propertyes;
using com.jds.GUpdater.classes.language;
using com.jds.GUpdater.classes.language.enums;
using com.jds.GUpdater.classes.version_control.gui;

namespace com.jds.GUpdater.classes.forms
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
            PropertyPage ppage = new PropertyPage(RConfig.Instance) { Location = new Point(3, 3) };
            _generalPage.Controls.Add(ppage);

            _tabs.TabPages.Add(_generalPage);

            foreach (object enu in Enum.GetValues(typeof (Game)))
            {
                var gpage = new TabPage(GameInfo.getNameOf(enu));
                GameProperty gp = RConfig.Instance.getGameProperty((Game) enu);
                var gppage = new PropertyPage(gp) {Location = new Point(3, 3), Enabled = gp.isEnable()};


                gpage.Controls.Add(gppage);

                _tabs.TabPages.Add(gpage);
            }

            _versionControlPage = new TabPage(LanguageHolder.Instance()[WordEnum.VERSION_CONTROL]);
            AssemblyPage assemblyPage = AssemblyPage.Instance();
            assemblyPage.Location = new Point(3, 3);
            _versionControlPage.Controls.Add(assemblyPage);

            _tabs.TabPages.Add(_versionControlPage);

            Shown += assemblyPage.Instance_Shown;
        }

        private void PropertyForm_Load(object sender, EventArgs e)
        {
            ChangeLanguage();
        }

        private void PropertyForm_Closing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance.ChangeLanguage();
            MainForm.Instance.CheckInstalled(true);
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