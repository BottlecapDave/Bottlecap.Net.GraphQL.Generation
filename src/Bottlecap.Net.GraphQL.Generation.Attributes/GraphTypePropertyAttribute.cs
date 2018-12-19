using System;

namespace Bottlecap.Net.GraphQL.Generation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GraphTypePropertyAttribute : Attribute
    {
        public string Name { get; set; }

        public bool IsIgnored { get; set; }
        
        public NullableBoolean IsNullable { get; set; }
    }
}
