using System.Diagnostics;
using System.IO;
using com.jds.AWLauncher.classes.forms;
using com.jds.AWLauncher.classes.language.enums;
using com.jds.AWLauncher.classes.listloader.enums;

namespace com.jds.AWLauncher.classes.games.propertyes.impl
{
    public class Lineage2Property : GameProperty
    {
        public override Game GameEnum()
        {
            return Game.LINEAGE2;
        }

        public override ProcessStartInfo GetStartInfo()
        {
            if (!Installed || !isEnable() || !_loader.IsValid || !this[ListFileType.CRITICAL])
            {
                return null;
            }

            string path = Path + "\\system_my\\l2.exe";

            if (!File.Exists(path))
            {
                MainForm.Instance.UpdateStatusLabel(WordEnum.S1_NOT_EXISTS_PLEASE_RECHECK, "l2.exe");
                return null;
            }

            var info = new ProcessStartInfo(path);

            return info;
        }

        public override string listURL()
        {
            return "http://localhost";
        }

        public override bool isEnable()
        {
            return false;
        }

        public override string getKey()
        {
            return "Software\\J Develop Station\\GUpdater\\Lineage 2";
        }
    }
}