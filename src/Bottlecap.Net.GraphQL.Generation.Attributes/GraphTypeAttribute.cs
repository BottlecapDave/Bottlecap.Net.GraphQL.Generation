using System;

namespace Bottlecap.Net.GraphQL.Generation.Attributes
{
    public class GraphTypeAttribute : Attribute
    {
        public bool IsInput { get; set; }
    }
}
