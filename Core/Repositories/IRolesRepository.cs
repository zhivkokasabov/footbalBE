using Core.Contracts.Response;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRolesRepository: IRepository<Role>
    {
        public Task<List<RoleDto>> GetRoles();
    }
}
