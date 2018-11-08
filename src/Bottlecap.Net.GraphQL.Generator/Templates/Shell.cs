using System.Collections.Generic;

namespace Bottlecap.Net.GraphQL.Generator.Templates
{
    public class Shell : BaseTemplate
    {
        public string Namespace { get; set; }

        public List<BaseType> Classes { get; private set; }

        public Shell()
        {
            Classes = new List<BaseType>();
        }
    }
}
