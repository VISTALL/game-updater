using System;

namespace com.jds.AWLauncher.classes.games.attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumName : Attribute
    {
        public EnumName(String s)
        {
            Root = s;
        }

        public String Root { get; set; }
    }
}