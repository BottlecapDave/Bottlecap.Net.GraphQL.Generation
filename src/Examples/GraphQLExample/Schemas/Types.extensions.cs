using GraphQL.DataLoader;
using GraphQLExample.Data;

namespace GraphQLExample.Schemas
{
    public partial class ToDoListGraphType
    {
        public ToDoListGraphType(IDataLoaderContextAccessor accessor, IUserRepository userRepository)
            : this()
        {
            // Example of extending a generate graph type with additional resolvers
            Field<UserGraphType, User>().Name("CreationUser").Description("The user who created the list").ResolveAsync(context =>
            {
                return accessor.GetUsersByIdsAsync(userRepository, () => context.Source.CreationUserId);
            });
        }
    }
}
