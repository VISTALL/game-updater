using com.jds.GUpdater.classes.language;
using com.jds.GUpdater.classes.language.enums;

namespace com.jds.GUpdater.classes.forms
{
    public partial class PropertyForm
    {
        public void ChangeLanguage()
        {
            Text = LanguageHolder.Instance[WordEnum.SETTINGS];
        }
    }
}