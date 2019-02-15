using System;

namespace Bottlecap.Net.GraphQL.Generation.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class InputGraphTypeAttribute : GraphTypeAttribute
    {
        public InputGraphTypeAttribute()
            : base()
        {
            IsInput = true;
        }

        public InputGraphTypeAttribute(string name)
            : base(name)
        {
            IsInput = true;
        }
    }
}
