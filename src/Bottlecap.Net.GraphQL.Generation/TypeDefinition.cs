using Bottlecap.Net.GraphQL.Generation.Attributes;
using System;
using System.Collections.Generic;

namespace Bottlecap.Net.GraphQL.Generation
{
    public class TypeDefinition
    {
        public Type Type { get; private set; }

        public List<PropertyDefinition> PropertyDefinitions { get; set; }

        public bool IsInput { get; set; }

        public bool IsDescriptionGenerated { get; set; }

        public TypeDefinition(Type type, GraphTypeAttribute attribute)
            : this(type)
        {
            IsInput = attribute.IsInput;
            IsDescriptionGenerated = attribute.IsDescriptionGenerated;
        }

        public TypeDefinition(Type type)
        {
            Type = type;

            PropertyDefinitions = new List<PropertyDefinition>();
            var properties = Type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var property in properties)
            {
                PropertyDefinitions.Add(new PropertyDefinition(this, property));
            }
        }
    }
}
