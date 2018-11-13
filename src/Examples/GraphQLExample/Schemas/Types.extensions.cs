using GraphQL.DataLoader;
using GraphQLExample.Data;

namespace GraphQLExample.Schemas
{
    public partial class ToDoListGraphType
    {
        public ToDoListGraphType(IDataLoaderContextAccessor accessor)
            : this()
        {
            // Example of extending a generate graph type with additional resolvers
            Field<UserGraphType, User>().Name("CreationUser").Description("The user who created the list").Resolve(context =>
            {
                return new User()
                {
                    Id = 1000,
                    Username = "Dave",
                    Password = "Password"
                };
            });
        }
    }
}
