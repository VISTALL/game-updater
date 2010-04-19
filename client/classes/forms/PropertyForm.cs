using System;
using System.Drawing;
using System.Windows.Forms;
using com.jds.GUpdater.classes.assembly;
using com.jds.GUpdater.classes.config;
using com.jds.GUpdater.classes.config.gui;
using com.jds.GUpdater.classes.games;
using com.jds.GUpdater.classes.games.propertyes;
using com.jds.GUpdater.classes.language;
using com.jds.GUpdater.classes.language.enums;

namespace com.jds.GUpdater.classes.forms
{
    public partial class PropertyForm : Form
    {
        private static PropertyForm _instance;

        #region Instance

        public static PropertyForm Instance
        {
            get { return _instance ?? (_instance = new PropertyForm()); }
        }

        #endregion

        #region Конструктор и главные методы формы

        public PropertyForm()
        {
            InitializeComponent();

            //1 вкладка главные настройки
            var page = new TabPage(LanguageHolder.Instance[WordEnum.GENERAL]);
            var ppage = new PropertyPage(RConfig.Instance) {Location = new Point(3, 3)};
            page.Controls.Add(ppage);

            _tabs.TabPages.Add(page);

            foreach (object enu in Enum.GetValues(typeof (Game)))
            {
                var gpage = new TabPage(GameInfo.getNameOf(enu));
                GameProperty gp = RConfig.Instance.getGameProperty((Game) enu);
                var gppage = new PropertyPage(gp) {Location = new Point(3, 3), Enabled = gp.isEnable()};


                gpage.Controls.Add(gppage);

                _tabs.TabPages.Add(gpage);
            }


            page = new TabPage("Assembly");
            ppage = new PropertyPage(AssemblyInfo.Instance) {Location = new Point(3, 3)};
            page.Controls.Add(ppage);

            _tabs.TabPages.Add(page);
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