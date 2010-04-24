using System.Diagnostics;
using System.IO;

namespace com.jds.GUpdater.classes.games.propertyes.impl
{
    public class Lineage2Property : GameProperty
    {
        public override Game GameEnum()
        {
            return Game.LINEAGE2;
        }

        public override ProcessStartInfo GetStartInfo()
        {
            if (!Installed || !isEnable() || !_loader.IsValid)
            {
                return null;
            }

            string path = Path + "\\system_my\\l2.exe";

            if (!File.Exists(path))
                return null;

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