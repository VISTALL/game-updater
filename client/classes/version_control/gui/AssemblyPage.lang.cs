using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.language.enums;

namespace com.jds.AWLauncher.classes.version_control.gui
{
  public partial class AssemblyPage
  {
      public void ChangeLanguage()
      {
          _versionLabel.Text = LanguageHolder.Instance()[WordEnum.VERSION];
          _currentVersionLabel.Text = LanguageHolder.Instance()[WordEnum.VERSION_ON_SERVER];
          _checkButton.Text = LanguageHolder.Instance()[WordEnum.CHECK];
          _updateBtn.Text = LanguageHolder.Instance()[WordEnum.UPDATE];
          Refresh();
      }
  }
}