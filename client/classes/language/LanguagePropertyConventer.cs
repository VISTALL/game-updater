using System;
using System.ComponentModel;
using System.Globalization;

namespace com.jds.AWLauncher.classes.language
{
    public class LanguagePropertyConventer : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType)
        {
            if (value is string)
            {
                var lang = value as string;
                bool has = false;
                foreach (string l in LanguageHolder.Instance().Languages)
                {
                    if (l.Equals(lang))
                    {
                        has = true;
                    }
                }

                if (!has)
                {
                    value = "English";
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}