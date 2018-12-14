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

                return "";
            }
        }

        public bool IsNullable
        {
            get
            {
                if (_definition.Overrides.IsNullable || Nullable.GetUnderlyingType(_definition.Property.PropertyType) != null)
                {
                    return true;
                }

                return false;
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