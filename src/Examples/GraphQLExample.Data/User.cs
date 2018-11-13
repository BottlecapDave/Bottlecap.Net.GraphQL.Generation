using Bottlecap.Net.GraphQL.Generation.Attributes;
using System.ComponentModel;

namespace GraphQLExample.Data
{
    [GraphType]
    public class User
    {
        [Description("The id of the user")]
        public long Id { get; set; }

        [Description("The username of the user")]
        public string Username { get; set; }

        [GraphTypeProperty(IsIgnored = true)]
        [Description("The password of the user")]
        public string Password { get; set; }
    }
}
