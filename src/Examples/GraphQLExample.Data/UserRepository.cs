using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLExample.Data
{
    public class UserRepository : IUserRepository
    {
        public Task<IDictionary<long, User>> GetUsersByIdsAsync(IEnumerable<long> ids)
        {
            // In the real world, this would probably come from a database

            var items = new Dictionary<long, User>();

            items.Add(1000, new User()
            {
                Id = 1000,
                Username = "Dave",
                Password = "Password"
            });

            items.Add(1001, new User()
            {
                Id = 1001,
                Username = "Bob",
                Password = "Password"
            });

            return Task.FromResult<IDictionary<long, User>>(items);
        }
    }
}
