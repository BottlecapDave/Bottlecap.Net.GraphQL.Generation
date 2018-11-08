using System;
using System.Collections.Generic;

namespace Bottlecap.Net.GraphQL.Generator.Templates
{
    public abstract class BaseType : BaseTemplate
    {
        private readonly Type _type;

        public string Name
        {
            get
            {
                return _type.Name;
            }
        }

        public string Namespace
        {
            get
            {
                return _type.Namespace;
            }
        }
        
        public List<BaseFieldType> Fields { get; private set; }

        public BaseType(Type type)
        {
            _type = type;
            Fields = new List<BaseFieldType>();

            var properties = _type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var property in properties)
            {
                if (((property.PropertyType.IsValueType == false) && property.PropertyType != typeof(string)) ||
                    property.PropertyType == typeof(DateTime))
                {
                    Fields.Add(new ExplicitFieldType(property));
                }
                else
                {
                    Fields.Add(new BaseFieldType(property));
                }
            }
        }
    }
}
