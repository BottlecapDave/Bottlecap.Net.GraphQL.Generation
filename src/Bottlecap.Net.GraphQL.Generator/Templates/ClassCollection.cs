using System.Collections.Generic;
using System.Text;

namespace Bottlecap.Net.GraphQL.Generator.Templates
{
    public class ClassCollection
    {
        public List<BaseType> Classes { get; private set; }

        public ClassCollection()
        {
            Classes = new List<BaseType>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var item in Classes)
            {
                builder.AppendLine(item.ToString());
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}