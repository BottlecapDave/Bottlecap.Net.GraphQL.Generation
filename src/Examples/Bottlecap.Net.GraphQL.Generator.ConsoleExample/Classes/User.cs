using System;
using System.ComponentModel;

namespace Bottlecap.Net.GraphQL.Generator.ConsoleExample.Classes
{
    public class User
    {
        [Description("The id of the user")]
        public long Id { get; set; }

        [Description("The username of the user")]
        public string Username { get; set; }

        [Description("The password of the user")]
        public string Password { get; set; }
    }
}
