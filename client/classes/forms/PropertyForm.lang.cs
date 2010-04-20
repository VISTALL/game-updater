using com.jds.GUpdater.classes.language;
using com.jds.GUpdater.classes.language.enums;

namespace com.jds.GUpdater.classes.forms
{
    public partial class PropertyForm
    {
        public void ChangeLanguage()
        {
            Text = LanguageHolder.Instance[WordEnum.SETTINGS];
            _versionControl.Text = LanguageHolder.Instance[WordEnum.VERSION_CONTROL];
            _versionControl.Refresh();
            Refresh();
        }
    }
}