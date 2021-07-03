using Core.Contracts.Request.Teams;
using Core.Contracts.Response.Teams;
using Core.Models;
using Core.Repositories;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TeamsService : ITeamsService
    {
        public IUnitOfWork UnitOfWork { get; }

        public TeamsService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<Team> CreateTeam(TeamDto team, int userId)
        {
            var user = await UnitOfWork.Users.GetUserAsync(userId);
            var newTeam = new Team
            {
                Name = team.Name,
                EntryKey = CreateRandomString(),
                Members = new List<User> { user }
            };

            user.IsTeamCaptain = true;

            await UnitOfWork.Teams.AddAsync(newTeam);
            await UnitOfWork.CommitAsync();

            return newTeam;
        }

        public async Task<Team> GetUserTeam(int userId)
        {
            return await UnitOfWork.Teams.GetUserTeam(userId);
        }

        public async Task<Team> GetTeam(int teamId)
        {
            return await UnitOfWork.Teams.GetTeam(teamId);
        }

        public async Task<Team> AddUserToTeam(int userId, string entryKey)
        {
            var user = await UnitOfWork.Users.GetByIdAsync(userId);
            var team = await UnitOfWork.Teams.FindAsync(entryKey);

            if (team == null)
            {
                return null;
            }

            team.Members.Add(user);

            await UnitOfWork.CommitAsync();

            return team;
        }

        public async Task<List<TournamentMatch>> GetTeamMatches(int teamId)
        {
            return await UnitOfWork.Teams.GetTeamMatches(teamId);
        }

        private string CreateRandomString()
        {
            var stringLength = 8;
            var rd = new Random();
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
