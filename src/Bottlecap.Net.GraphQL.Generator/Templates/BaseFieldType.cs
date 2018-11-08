using System;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generator.Templates
{
    public class BaseFieldType : BaseTemplate
    {
        protected readonly PropertyInfo _property;

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

        public string IsNullable
        {
            get
            {
                if (Nullable.GetUnderlyingType(_property.PropertyType) != null)
                {
                    return "true";
                }

                return "false";
            }
        }

        public BaseFieldType(PropertyInfo property)
        {
            _property = property;
        }
    }
}