using Core.Contracts.Request;
using Core.Contracts.Response;
using Core.Contracts.Response.Users;
using Core.InternalObjects;
using Core.Models;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUsersService
    {
        Task<LoginDto> CreateUser(UserDto user);
        Task<LoginDto> Login(UserDto user);
        Task<UserOutputDto> GetUser(string userName);
        Task<UpdateProfileModel> UpdateUser(UserDto user);
        string GenerateToken(User user);
    }
}
