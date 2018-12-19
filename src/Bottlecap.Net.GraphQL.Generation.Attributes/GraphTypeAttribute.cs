using System;

namespace Bottlecap.Net.GraphQL.Generation.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class GraphTypeAttribute : Attribute
    {
        public bool IsInput { get; set; }

        public bool IsDescriptionGenerated { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public GraphTypeAttribute()
        {
            IsDescriptionGenerated = true;
        }
    }
}
