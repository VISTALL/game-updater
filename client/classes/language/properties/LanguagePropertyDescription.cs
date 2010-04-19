using System;
using System.ComponentModel;
using com.jds.GUpdater.classes.language.enums;

namespace com.jds.GUpdater.classes.language.properties
{
    public class LanguagePropertyDescription : PropertyDescriptor
    {
        private readonly PropertyDescriptor _basePropertyDescriptor;
        private WordEnum? _descriptionWord;
        private WordEnum? _nameWord;

        public LanguagePropertyDescription(PropertyDescriptor basePropertyDescriptor) : base(basePropertyDescriptor)
        {
            _basePropertyDescriptor = basePropertyDescriptor;
        }

        public override Type ComponentType
        {
            get { return _basePropertyDescriptor.ComponentType; }
        }

        public override string DisplayName
        {
            get
            {
                foreach (object attribute in _basePropertyDescriptor.Attributes)
                {
                    if (attribute.GetType().Equals(typeof (LanguageDisplayName)))
                    {
                        _nameWord = ((LanguageDisplayName) attribute).Word;
                    }
                }

                return _nameWord == null
                           ? _basePropertyDescriptor.DisplayName
                           : LanguageHolder.Instance[(WordEnum) _nameWord];
            }
        }

        public override string Description
        {
            get
            {
                foreach (object attribute in _basePropertyDescriptor.Attributes)
                {
                    if (attribute.GetType().Equals(typeof (LanguageDescription)))
                    {
                        _descriptionWord = ((LanguageDescription) attribute).Word;
                    }
                }

                return _descriptionWord == null
                           ? _basePropertyDescriptor.DisplayName
                           : LanguageHolder.Instance[(WordEnum) _descriptionWord];
            }
        }

        public override bool IsReadOnly
        {
            get { return _basePropertyDescriptor.IsReadOnly; }
        }

        public override string Name
        {
            get { return _basePropertyDescriptor.Name; }
        }

        public override Type PropertyType
        {
            get { return _basePropertyDescriptor.PropertyType; }
        }

        public override bool CanResetValue(object component)
        {
            return _basePropertyDescriptor.CanResetValue(component);
        }

        public override object GetValue(object component)
        {
            return _basePropertyDescriptor.GetValue(component);
        }

        public override void ResetValue(object component)
        {
            _basePropertyDescriptor.ResetValue(component);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return _basePropertyDescriptor.ShouldSerializeValue(component);
        }

        public override void SetValue(object component, object value)
        {
            _basePropertyDescriptor.SetValue(component, value);
        }
    }
}