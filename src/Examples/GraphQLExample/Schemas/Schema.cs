using GraphQL;

namespace GraphQLExample.Schemas
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<Query>();
        }
    }
}
