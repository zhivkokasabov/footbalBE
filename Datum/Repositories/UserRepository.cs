using Core.Contracts.Response.Users;
using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datum.Repositories
{
    class UserRepository: Repository<User>, IUsersRepository
    {
        public UserRepository(FootballManagerDbContext context)
            : base(context)
        {
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await DbContext.AddAsync(user);

            return user;
        }

        public async Task<User> GetUserAsync(string email)
        {
            var user = await DbContext.Users.Where(x => x.Email == email)
                .Select(x => new User
                {
                    Nickname = x.Nickname,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Id = x.Id,
                    IsTeamCaptain = x.IsTeamCaptain,
                    UserPositions = x.UserPositions.Select(up => new UserPosition
                    {
                        PlayerPosition = up.PlayerPosition
                    }).ToList(),
                    UserRoles = x.UserRoles.Select(ur => new UserRole
                    {
                        Role = ur.Role
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            // temporary work around. I can't figure out why ef can't save userroles navigation prop
            // and i lost a lot of time so left it for later
            var roleIds = await DbContext.UserRoles
                .Where(x => x.UserId == user.Id)
                .Select(x => x.RoleId)
                .ToListAsync();

            var roles = await DbContext.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync();

            user.UserRoles = roles.Select(x => new UserRole { Role = x }).ToList();

            return user;
        }



        public async Task<User> GetUserAsync(int userId)
        {
            return await DbContext.Users.FindAsync(userId);
        }

        public async Task<List<UserPosition>> GetUserPositionsAsync(int userId)
        {
            return await DbContext.UserPositions.Where(x => x.Active && x.UserId == userId).ToListAsync();
        }
    }
}
