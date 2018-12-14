using System;

namespace Bottlecap.Net.GraphQL.Generation.Attributes
{
    public class GraphTypePropertyAttribute : Attribute
    {
        public string Name { get; set; }

        public bool IsIgnored { get; set; }
        
        public bool IsNullable { get; set; }
    }
}
