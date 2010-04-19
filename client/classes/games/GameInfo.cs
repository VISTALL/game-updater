using System;

namespace com.jds.GUpdater.classes.games
{
    public class GameInfo
    {
        public static String getNameOf(Object g)
        {
            var r =
                (EnumName) g.GetType().GetField(g.ToString()).GetCustomAttributes(typeof (EnumName), false).GetValue(0);
            return r.Root;
        }
    }
}