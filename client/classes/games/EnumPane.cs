using System;

namespace com.jds.GUpdater.classes.games
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