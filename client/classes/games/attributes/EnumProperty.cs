using System;

namespace com.jds.GUpdater.classes.games.attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumProperty : Attribute
    {
        public EnumProperty(Type t)
        {
            Type = t;
        }

        public Type Type { get; set; }
    }
}