using System;
using System.Reflection;
using com.jds.AWLauncher.classes.language.properties;
using com.jds.AWLauncher.classes.registry.attributes;
using Microsoft.Win32;

namespace com.jds.AWLauncher.classes.registry
{
    public abstract class RegistryProperty : LanguageCustomTypeDescription
    {
        public void select()
        {
            var key = Registry.CurrentUser;
            key = key.OpenSubKey(getKey());

            PropertyInfo[] pps = GetType().GetProperties();

            foreach (PropertyInfo property in pps)
            {
                if (property.IsDefined(typeof (RegistryPropertyKey), true))
                {
                    RegistryPropertyKey registryPropertyKey =
                        (RegistryPropertyKey) property.GetCustomAttributes(typeof (RegistryPropertyKey), true)[0];

                    String val = registryPropertyKey.Default == null ? null : registryPropertyKey.Default.ToString();
                    
                    if(key != null)
                    {
                        val = (String)key.GetValue(registryPropertyKey.Root,
                                              registryPropertyKey.Default == null
                                                  ? null
                                                  : registryPropertyKey.Default.ToString());  
                    }
                   
                    if (val != null)
                    {
                        if (registryPropertyKey.Default is Boolean)
                        {
                            property.SetValue(this, Boolean.Parse(val), null);
                        }
                        else if (registryPropertyKey.Default is Enum)
                        {
                            property.SetValue(this, Enum.Parse(registryPropertyKey.Default.GetType(), val),
                                              null);
                        }
                        else if (registryPropertyKey.Default is Int32)
                        {
                            property.SetValue(this, Int32.Parse(val), null);
                        }
                        else
                        {
                            property.SetValue(this, val, null);
                        }
                    }
                    else
                    {
                        property.SetValue(this, null, null);   
                    }
                }
            }
        }

        public void insert()
        {
            var key = Registry.CurrentUser;
            key = key.CreateSubKey(getKey());

            PropertyInfo[] pps = GetType().GetProperties();
           
            foreach (PropertyInfo property in pps)
            {
                if (property.IsDefined(typeof(RegistryPropertyKey), true))
                {
                    RegistryPropertyKey registryPropertyKey =
                        (RegistryPropertyKey) property.GetCustomAttributes(typeof (RegistryPropertyKey), true)[0];

                    if (key != null)
                    {
                        key.SetValue(registryPropertyKey.Root, property.GetValue(this, null).ToString());
                    }
                }
            }
        }

        public abstract String getKey();
    }
}