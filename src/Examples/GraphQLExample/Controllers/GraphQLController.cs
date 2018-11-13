using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL.Types;
using GraphQL;
using GraphQL.Execution;

namespace GraphQLExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        private readonly IDocumentExecutionListener _listener;

        public GraphQLController(ISchema schema, IDocumentExecuter executer, IDocumentExecutionListener listener)
        {
            _schema = schema;
            _executer = executer;
            _listener = listener;
        }

        [HttpGet]
        public async Task<object> GetQuery(string query)
        {
            return await PostQuery(new GraphQLRequest() { Query = query });
        }

        [HttpPost]
        public async Task<object> PostQuery([FromBody] GraphQLRequest request)
        {
            var inputs = request.Variables.ToInputs();
            var options = new ExecutionOptions()
            {
                Query = request.Query,
                Schema = _schema,
                UserContext = User,
                Inputs = inputs
            };

            options.Listeners.Add(_listener);

            var result = await _executer.ExecuteAsync(options).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return result;
        }
    }
}
