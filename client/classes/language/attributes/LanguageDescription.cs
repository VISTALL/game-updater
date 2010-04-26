using System;
using com.jds.AWLauncher.classes.language.enums;

namespace com.jds.AWLauncher.classes.language.attributes
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