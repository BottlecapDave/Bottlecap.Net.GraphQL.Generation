using System;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class BaseFieldType : BaseTemplate
    {
        protected readonly PropertyInfo _property;
        protected readonly PropertyDefinition _definition;

        public string Name
        {
            get
            {
                return _property.Name;
            }
        }

        public string Description
        {
            get
            {
                var attribute = _property.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>();
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
                if (_definition.IsNullable || Nullable.GetUnderlyingType(_property.PropertyType) != null)
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

        public BaseFieldType(PropertyInfo property, PropertyDefinition definition)
        {
            _property = property;
            _definition = definition;
        }
    }
}