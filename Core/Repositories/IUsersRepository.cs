using Core.Models;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        public Task<User> CreateUserAsync(User user);
        public Task<User> GetUserAsync(string email);
        public Task<User> GetUserAsync(int userId);
    }
}
