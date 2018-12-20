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

        public string Name { get; set; }

        public bool IsDescriptionGenerated { get; set; }

        public TypeDefinition(Type type, GraphTypeAttribute attribute)
            : this(type)
        {
            if (String.IsNullOrEmpty(attribute.Name) == false)
            {
                Name = attribute.Name;
            }

            IsInput = attribute.IsInput;
            IsDescriptionGenerated = attribute.IsDescriptionGenerated;
        }

        public TypeDefinition(Type type)
        {
            Type = type;
            Name = type.Name;

            PropertyDefinitions = new List<PropertyDefinition>();
            var properties = Type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var property in properties)
            {
                PropertyDefinitions.Add(new PropertyDefinition(this, property));
            }
        }
    }
}
