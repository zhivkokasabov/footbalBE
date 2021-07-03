using Core.Contracts.Response;
using Core.Models;
using Core.Repositories;
using Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class RoleService : IRolesService
    {
        public IUnitOfWork UnitOfWork { get; }

        public RoleService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            return await UnitOfWork.Roles.GetRoles();
        }
    }
}
