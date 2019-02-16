using System.Collections.Generic;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class Shell : BaseTemplate
    {
        public string Namespace { get; set; }

        public List<BaseTemplate> Classes { get; private set; }

        public Shell(Generator generator)
            : base(generator)
        {
            Classes = new List<BaseTemplate>();
        }
    }
}
