using Core.Contracts.Response;
using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Repositories
{
    public class RoleRepository : Repository<Role>, IRolesRepository
    {
        public RoleRepository(FootballManagerDbContext context):
            base(context)
        { }
        
        public async Task<List<RoleDto>> GetRoles()
        {
            return await DbContext.Roles
                .Where(x => x.NormalizedName != "ADMIN")
                .Select(x => new RoleDto
                {
                    RoleId = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}
