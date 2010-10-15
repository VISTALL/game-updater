using System;
using com.jds.AWLauncher.classes.language.enums;

namespace com.jds.AWLauncher.classes.language.attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LanguagePicture : Attribute
    {
        public LanguagePicture(PictureName a)
        {
            Picture = a;
        }

        public PictureName Picture { get; private set; }
    }
}
