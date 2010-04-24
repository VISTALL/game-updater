using System;

namespace com.jds.GUpdater.classes.registry.attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RegistryPropertyKey : Attribute
    {
        public RegistryPropertyKey(String s, Object defaul)
        {
            Root = s;
            Default = defaul;
        }

        public Object Default { set; get; }

        public String Root { get; set; }
    }
}