using System;

namespace Bottlecap.Net.GraphQL.Generation.Attributes
{
    public class GraphTypePropertyAttribute : Attribute
    {
        public bool IsIgnored { get; set; }
    }
}
