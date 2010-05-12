using System;
using System.Drawing;
using com.jds.AWLauncher.classes.config;
using com.jds.AWLauncher.classes.games;
using com.jds.AWLauncher.classes.games.propertyes;
using com.jds.AWLauncher.classes.images;
using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.language.enums;
using com.jds.AWLauncher.classes.listloader.enums;

namespace com.jds.AWLauncher.classes.forms
{
    public sealed partial class MainForm
    {
        public const int DIFF = 5;

        public void ChangeLanguage(bool onStart)
        {
            if (!onStart)
            {
                foreach (object p in Enum.GetValues(typeof (Game)))
                {
                    GameProperty gameProperty = RConfig.Instance.getGameProperty((Game) p);
                    gameProperty.ListLoader.IsValid = false;

                    gameProperty.ListLoader.Items[ListFileType.CRITICAL].Clear();
                    gameProperty.ListLoader.Items[ListFileType.NORMAL].Clear();
                }
            }

            Text = LanguageHolder.Instance()[WordEnum.TITLE];

            _infoStart.Text = LanguageHolder.Instance()[WordEnum.START_INFO];
            _selectGameLabel.Text = LanguageHolder.Instance()[WordEnum.GAMES];
            _lastNews.Text = LanguageHolder.Instance()[WordEnum.LAST_NEWS];

            _startButton.Info = ImageHolder.Instance()[PictureName.START];
            _closeBtn.Info = ImageHolder.Instance()[PictureName.CLOSE];
            _fullCheck.Info = ImageHolder.Instance()[PictureName.FULLCHECK];
            _settingsButton.Info = ImageHolder.Instance()[PictureName.SETTINGS];
            _minimizedButton.Info = ImageHolder.Instance()[PictureName.MINI];

            var Y = _homePage.Location.Y;
            var sepDiff = _separator1.Width / 2;

            _homePage.Text = LanguageHolder.Instance()[WordEnum.HOMEPAGE];

            _separator1.Location = new Point(_homePage.Width + _homePage.Location.X + DIFF - sepDiff, Y);

            _faqLabel.Location = new Point(sepDiff + _separator1.Location.X + DIFF, Y);
            _faqLabel.Text = LanguageHolder.Instance()[WordEnum.FAQ];

            _separator2.Location = new Point(_faqLabel.Width + _faqLabel.Location.X + DIFF - sepDiff, Y);

            _forumLabel.Location = new Point(sepDiff + _separator2.Location.X + DIFF, Y);
            _forumLabel.Text = LanguageHolder.Instance()[WordEnum.FORUM];

            _separator3.Location = new Point(_forumLabel.Width + _forumLabel.Location.X + DIFF - sepDiff, Y);

            _joinNowLabel.Location = new Point(sepDiff + _separator3.Location.X + DIFF, Y);
            _joinNowLabel.Text = LanguageHolder.Instance()[WordEnum.JOIN_NOW];

            SetVersionType(Version, VersionType);
        }
    }
}