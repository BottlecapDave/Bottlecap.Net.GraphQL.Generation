using GraphQL.Types;
using GraphQLExample.Data;
using System;
using System.Collections.Generic;

namespace GraphQLExample.Schemas
{
    public class Query : ObjectGraphType
    {
        public Query()
        {
            Field<ListGraphType<ToDoListGraphType>, IEnumerable<ToDoList>>("todolists")
                .Description("Retrieves an instance of a to do list")
                .Resolve(x =>
                {
                    // In the real world, this would be loaded from a database
                    var items = new List<ToDoList>();

                    items.Add(new ToDoList()
                    {
                        Id = 2000,
                        Name = "Shopping List",
                        Tags = new string[] { "shopping" },
                        CreationUserId = 1000,
                        CreationTimestamp = DateTime.UtcNow
                    });

                    items.Add(new ToDoList()
                    {
                        Id = 2001,
                        Name = "Shopping List 2",
                        Tags = new string[] { "shopping" },
                        CreationUserId = 1001,
                        CreationTimestamp = DateTime.UtcNow
                    });

                    items.Add(new ToDoList()
                    {
                        Id = 2002,
                        Name = "Shopping List 3",
                        Tags = new string[] { "shopping" },
                        CreationUserId = 1000,
                        CreationTimestamp = DateTime.UtcNow
                    });

                    return items;
                });
        }
    }
}
