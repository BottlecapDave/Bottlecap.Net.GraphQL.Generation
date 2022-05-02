using GraphQL;

namespace GraphQLExample.Schemas
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            Query = new Query();
        }
    }
}
