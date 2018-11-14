using Bottlecap.Net.GraphQL.Generation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GraphQLExample.Data
{
    [GraphType]
    public class ToDoList
    {
        [Description("The id of the to do list")]
        public long Id { get; set; }

        [Description("The name of the do list")]
        public string Name { get; set; }

        // This is demonstrating that a complex type can be referenced
        [Description("The user who owns the list")]
        public User User { get; set; }

        // This is demonstrating that a list of items can be referenced
        [Description("The tags associated with the to do list")]
        public string[] Tags { get; set; }

        // This is demonstrating that a complex type can be referenced
        [Description("The list of users who are owners of the list")]
        public IEnumerable<User> Owners { get; set; }

        [Description("The current state of the to do list")]
        public ToDoListState State { get; set; }

        [Description("The id of the user who created this to do list")]
        public long? CreationUserId { get; set; }

        [Description("The time at which the to do list was created")]
        public DateTime CreationTimestamp { get; set; }
    }
}
