using com.jds.AWLauncher.classes.games.propertyes;
using com.jds.AWLauncher.classes.gui;
using com.jds.AWLauncher.classes.gui.tabpane;
using com.jds.AWLauncher.classes.images;
using com.jds.AWLauncher.classes.language.enums;

namespace com.jds.AWLauncher.classes.games.gui
{
    public partial class SimpleGamePanel : JPanelTab, IGamePanel
    {
        private readonly GameProperty _property;

        public SimpleGamePanel(GameProperty prop)
        {
            _property = prop;
            _property.Panel = this;

            InitializeComponent();
            InitializeLanguage();
        }

        #region IGamePanel Members

        public void refreshNews()
        {
            rssPanel1.RefreshNews(StatusRSS);
        }

        public Game getGame()
        {
            return _property.GameEnum();
        }

        public void close()
        {
            _property.ListLoader.Cancel();
            rssPanel1.Close();
        }

        #endregion

        public override IImageInfo getTab()
        {
            switch (getGame())
            {
                case Game.AION:
                    return ImageHolder.Instance()[PictureName.AION];
                case Game.LINEAGE2:
                    return ImageHolder.Instance()[PictureName.LINEAGE2];
            }
            return null;
        }
    }
}