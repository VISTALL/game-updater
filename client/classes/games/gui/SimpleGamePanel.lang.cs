using com.jds.AWLauncher.classes.language;

namespace com.jds.AWLauncher.classes.games.gui
{
    public partial class SimpleGamePanel
    {
        public void InitializeLanguage()
        {
            switch (LanguageHolder.Instance().Language.ShortName)
            {
                case "en":
                    rssPanel1.URL = "http://aionwars.com/news-and-updates/rss.xml";
                    break;
                default:
                    rssPanel1.URL = "http://awars.net/news/rss.xml";
                    break;
            }
        }
    }
}