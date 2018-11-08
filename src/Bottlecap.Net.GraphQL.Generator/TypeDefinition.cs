using System;

namespace Bottlecap.Net.GraphQL.Generator
{
    public class TypeDefinition
    {
        public Type Type { get; private set; }

        public string[] PropertiesToIgnore { get; set; }

        public TypeDefinition(Type type)
        {
            Type = type;
        }
    }
}
