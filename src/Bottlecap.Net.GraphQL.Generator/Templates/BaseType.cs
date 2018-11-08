using System;
using System.Collections.Generic;
using System.Linq;

namespace Bottlecap.Net.GraphQL.Generator.Templates
{
    public abstract class BaseType : BaseTemplate
    {
        private readonly TypeDefinition _typeDefinition;

        public string Name
        {
            get
            {
                return _typeDefinition.Type.Name;
            }
        }

        public string Namespace
        {
            get
            {
                return _typeDefinition.Type.Namespace;
            }
        }
        
        public List<BaseFieldType> Fields { get; private set; }

        public BaseType(TypeDefinition type)
        {
            _typeDefinition = type;
            Fields = new List<BaseFieldType>();

            var properties = _typeDefinition.Type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var property in properties)
            {
                if (_typeDefinition.PropertiesToIgnore?.Any(x => String.Equals(x, property.Name, StringComparison.OrdinalIgnoreCase)) == true)
                {
                    continue;
                }

                if (((property.PropertyType.IsValueType == false) && property.PropertyType != typeof(string)))
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
