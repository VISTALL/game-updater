using System;

namespace com.jds.AWLauncher.classes.games.attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumPane : Attribute
    {
        public EnumPane(Type t)
        {
            Type = t;
        }

        public Type Type { get; set; }
    }
}