using System;
using com.jds.GUpdater.classes.language.enums;

namespace com.jds.GUpdater.classes.language.properties
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LanguageDisplayName : Attribute
    {
        public LanguageDisplayName(WordEnum a)
        {
            Word = a;
        }

        public WordEnum Word { get; private set; }
    }
}