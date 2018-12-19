using System;

namespace Bottlecap.Net.GraphQL.Generation.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class GraphTypeAttribute : Attribute
    {
        public bool IsInput { get; set; }
    }
}
