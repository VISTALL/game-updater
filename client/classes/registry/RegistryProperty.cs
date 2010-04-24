using System;
using com.jds.GUpdater.classes.language.properties;
using com.jds.GUpdater.classes.registry.attributes;
using log4net;
using Microsoft.Win32;

namespace com.jds.GUpdater.classes.registry
{
    public abstract class RegistryProperty : LanguageCustomTypeDescription
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof (RegistryProperty));

        public void select()
        {
            var key = Registry.CurrentUser;
            key = key.OpenSubKey(getKey());

            if (key != null)
            {
                var pps = GetType().GetProperties();
                foreach (var property in pps)
                {
                    foreach (Attribute a in property.GetCustomAttributes(true))
                    {
                        try
                        {
                            if (a is RegistryPropertyKey)
                            {
                                var attribute = a as RegistryPropertyKey;
                                var val = key.GetValue(attribute.Root, attribute.Default == null ? null : attribute.Default.ToString());

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
            var key = Registry.CurrentUser;
            key = key.CreateSubKey(getKey());

            var pps = GetType().GetProperties();
            foreach (var property in pps)
            {
                foreach (Attribute a in property.GetCustomAttributes(true))
                {
                    try
                    {
                        if (a is RegistryPropertyKey)
                        {
                            var attribute = a as RegistryPropertyKey;
                            if (key != null) key.SetValue(attribute.Root, property.GetValue(this, null).ToString());
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