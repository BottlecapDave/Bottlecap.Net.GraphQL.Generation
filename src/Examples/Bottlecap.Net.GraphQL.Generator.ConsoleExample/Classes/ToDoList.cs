using System;
using System.ComponentModel;

namespace Bottlecap.Net.GraphQL.Generator.ConsoleExample.Classes
{
    public class ToDoList
    {
        [Description("The id of the to do list")]
        public long Id { get; set; }

        [Description("The name of the do list")]
        public string Name { get; set; }

        [Description("The user who owns the list")]
        public User User { get; set; }

        [Description("The id of the user who created this to do list")]
        public long? CreationUserId { get; set; }

        [Description("The time at which the to do list was created")]
        public DateTime CreationUserTimestamp { get; set; }
    }
}
