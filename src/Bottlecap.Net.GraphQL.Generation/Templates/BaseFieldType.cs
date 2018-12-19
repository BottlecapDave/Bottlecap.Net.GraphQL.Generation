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
                return _definition.Overrides.Name ?? _definition.Property.Name;
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

                return _definition.Parent.IsDescriptionGenerated 
                       ? $"The {_definition.Property.Name.Humanize(LetterCasing.Sentence).ToLowerInvariant()} of {_definition.Property.ReflectedType.Name.Humanize(LetterCasing.Sentence).ToLowerInvariant()}"
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

        public BaseFieldType(PropertyDefinition definition)
        {
            _definition = definition;
        }
    }
}