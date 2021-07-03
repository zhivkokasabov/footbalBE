using Core.Contracts.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IRolesService
    {
        Task<List<RoleDto>> GetRoles();
    }
}
