using System;
using System.Reflection;
using com.jds.GUpdater.classes.language.properties;
using log4net;
using Microsoft.Win32;

namespace com.jds.GUpdater.classes.registry
{
    public abstract class RegistryProperty : LanguageCustomTypeDescription
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof (RegistryProperty));

        public void select()
        {
            RegistryKey key = Registry.CurrentUser;
            key = key.OpenSubKey(getKey());

            if (key != null)
            {
                PropertyInfo[] pps = GetType().GetProperties();
                foreach (PropertyInfo property in pps)
                {
                    foreach (Attribute a in property.GetCustomAttributes(true))
                    {
                        try
                        {
                            if (a is RegistryPropertyKey)
                            {
                                var attribute = a as RegistryPropertyKey;
                                object val = key.GetValue(attribute.Root, attribute.Default.ToString());

                                if (attribute.Default is Boolean)
                                {
                                    property.SetValue(this, Boolean.Parse((String) val), null);
                                }
                                else if (attribute.Default is Enum)
                                {
                                    property.SetValue(this, Enum.Parse(attribute.Default.GetType(), (String) val), null);
                                }
                                else if (attribute.Default is Int32)
                                {
                                    property.SetValue(this, Int32.Parse((String) val), null);
                                }
                                else
                                {
                                    property.SetValue(this, val, null);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            _log.Info("Exception: " + e, e);
                        }
                    }
                }
            }
        }

        public void insert()
        {
            RegistryKey key = Registry.CurrentUser;
            key = key.CreateSubKey(getKey());

            PropertyInfo[] pps = GetType().GetProperties();
            foreach (PropertyInfo property in pps)
            {
                foreach (Attribute a in property.GetCustomAttributes(true))
                {
                    try
                    {
                        if (a is RegistryPropertyKey)
                        {
                            var attribute = a as RegistryPropertyKey;
                            key.SetValue(attribute.Root, property.GetValue(this, null).ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        _log.Info("Exception: " + e, e);
                    }
                }
            }
        }

        public abstract String getKey();
    }
}