using System;
using com.jds.GUpdater.classes.language.enums;

namespace com.jds.GUpdater.classes.language.attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LanguageDescription : Attribute
    {
        public LanguageDescription(WordEnum a)
        {
            Word = a;
        }

        public WordEnum Word { get; private set; }
    }
}