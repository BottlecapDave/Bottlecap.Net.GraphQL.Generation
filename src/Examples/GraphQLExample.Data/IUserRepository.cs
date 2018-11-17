using Bottlecap.Net.GraphQL.Generation.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLExample.Data
{
    [DataLoaders]
    public interface IUserRepository
    {
        Task<IDictionary<long, User>> GetUsersByIdsAsync(IEnumerable<long> ids);
    }
}
