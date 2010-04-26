using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.language.enums;

namespace com.jds.AWLauncher.classes.forms
{
    public partial class PropertyForm
    {
        public void ChangeLanguage()
        {
            Text = LanguageHolder.Instance()[WordEnum.SETTINGS];
            _versionControlPage.Text = LanguageHolder.Instance()[WordEnum.VERSION_CONTROL];
            _generalPage.Text = LanguageHolder.Instance()[WordEnum.GENERAL];
        }
    }
}