using Bottlecap.Net.GraphQL.Generation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class GraphType : BaseTemplate
    {
        private readonly TypeDefinition _typeDefinition;

        public bool IsInput { get; set; }
        public bool IsOutput { get { return _typeDefinition.IsInput == false; } }

        public string ClassName
        {
            get
            {
                if (IsInput)
                {
                    return $"{Name}InputGraphType";
                }
                else
                {
                    return $"{Name}GraphType";
                }
            }
        }

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

        public GraphType(TypeDefinition type, bool isInput = false)
        {
            IsInput = isInput;
            _typeDefinition = type;
            Fields = new List<BaseFieldType>();

            var properties = _typeDefinition.Type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<GraphTypePropertyAttribute>();
                if (attribute != null && attribute.IsIgnored)
                {
                    continue;
                }

                var propertyDefiniton = _typeDefinition.Properties?.FirstOrDefault(x => String.Equals(x.Name, property.Name, StringComparison.OrdinalIgnoreCase));
                if (propertyDefiniton == null)
                {
                    propertyDefiniton = new PropertyDefinition(property.Name);
                }

                if (propertyDefiniton.IsIgnored == true)
                {
                    continue;
                }

                if (((property.PropertyType.IsValueType == false) && property.PropertyType != typeof(string)))
                {
                    Fields.Add(new ExplicitFieldType(property, propertyDefiniton));
                }
                else
                {
                    Fields.Add(new BaseFieldType(property, propertyDefiniton));
                }
            }
        }
    }
}
