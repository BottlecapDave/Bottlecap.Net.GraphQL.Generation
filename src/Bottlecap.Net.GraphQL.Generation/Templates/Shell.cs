using System.Collections.Generic;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class Shell : BaseTemplate
    {
        public string Namespace { get; set; }

        public List<IGraphType> Classes { get; private set; }

        public Shell()
        {
            Classes = new List<IGraphType>();
        }
    }
}
