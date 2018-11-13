using Newtonsoft.Json.Linq;

namespace GraphQLExample.Controllers
{
    public class GraphQLRequest
    {
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
