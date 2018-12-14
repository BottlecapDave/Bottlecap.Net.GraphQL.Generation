using Bottlecap.Net.GraphQL.Generation.Attributes;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation
{
    public class PropertyDefinition
    {
        public GraphTypePropertyAttribute Overrides { get; set; }

        public PropertyInfo Property { get; set; }

        public PropertyDefinition(PropertyInfo property)
        {
            Property = property;
            Overrides = property.GetCustomAttribute<GraphTypePropertyAttribute>() ?? new GraphTypePropertyAttribute();
        }
    }
}
