using GraphQL.Types;
using GraphQLExample.Data;
using System;

namespace GraphQLExample.Schemas
{
    public class Query : ObjectGraphType
    {
        public Query()
        {
            Field<ToDoListGraphType, ToDoList>("todolist")
                .Description("Retrieves an instance of a to do list")
                .Resolve(x => new ToDoList()
                {
                    Id = 2000,
                    Name = "Shopping List",
                    Tags = new string[] { "shopping" },
                    CreationUserId = 1000,
                    CreationTimestamp = DateTime.UtcNow
                });
        }
    }
}
