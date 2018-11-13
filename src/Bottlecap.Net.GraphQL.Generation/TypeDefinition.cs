using System;

namespace Bottlecap.Net.GraphQL.Generation
{
    public class TypeDefinition
    {
        public Type Type { get; private set; }

        public PropertyDefinition[] Properties { get; set; }

        public bool IsInput { get; set; }

        public TypeDefinition(Type type)
        {
            Type = type;
        }
    }
}
