using Bottlecap.Net.GraphQL.Generation.Attributes;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation
{
    public class PropertyDefinition
    {
        public TypeDefinition Parent { get; private set; }

        public GraphTypePropertyAttribute Overrides { get; private set; }

        public PropertyInfo Property { get; private set; }

        public PropertyDefinition(TypeDefinition parent, PropertyInfo property)
        {
            Parent = parent;

            Property = property;
            Overrides = property.GetCustomAttribute<GraphTypePropertyAttribute>() ?? new GraphTypePropertyAttribute();
        }
    }
}
