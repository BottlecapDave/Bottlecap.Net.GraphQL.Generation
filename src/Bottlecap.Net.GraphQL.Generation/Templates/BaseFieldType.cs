using Humanizer;
using System;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class BaseFieldType : BaseTemplate
    {
        protected readonly PropertyDefinition _definition;

        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty(_definition.Overrides.Name) == false && _definition.Parent.IsInput)
                {
                    throw new InvalidOperationException($"Name override of '{_definition.Overrides.Name}' is not supported for '{_definition.Parent.Name}' because it is an input graph type");
                }

                return _definition.Overrides.Name ?? _definition.Property.Name;
            }
        }

        public string PropertyName
        {
            get
            {
                return _definition.Property.Name;
            }
        }

        public string Description
        {
            get
            {
                var attribute = _definition.Property.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>();
                if (attribute != null)
                {
                    return attribute.Description;
                }

                if (String.IsNullOrEmpty(_definition.Overrides.Description) == false)
                {
                    return _definition.Overrides.Description;
                }

                return _definition.Parent.IsDescriptionGenerated 
                       ? $"The {_definition.Property.Name.Humanize(LetterCasing.Sentence).ToLowerInvariant()} of the {_definition.Parent.Name.Humanize(LetterCasing.Sentence).ToLowerInvariant()}"
                       : "";
            }
        }

        public bool IsNullable
        {
            get
            {
                switch (_definition.Overrides.IsNullable)
                {
                    case Attributes.NullableBoolean.True:
                        return true;
                    case Attributes.NullableBoolean.False:
                        return false;
                    default:
                        return Nullable.GetUnderlyingType(_definition.Property.PropertyType) != null;
                }
            }
        }

        public string IsNullableString
        {
            get
            {
                return IsNullable.ToString().ToLowerInvariant();
            }
        }

        public BaseFieldType(Generator generator, PropertyDefinition definition)
            : base(generator) 
        {
            _definition = definition;
        }
    }
}