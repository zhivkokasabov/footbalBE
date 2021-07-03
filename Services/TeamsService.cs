using Core.Contracts.Request.Teams;
using Core.Contracts.Response.Teams;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TeamsService : ITeamsService
    {
        public IUnitOfWork UnitOfWork { get; }
        private readonly string UserImagesPath;
        private readonly string ImageFileName;

        public TeamsService(
            IUnitOfWork unitOfWork,
            IConfiguration configuration)
        {
            UnitOfWork = unitOfWork;
            UserImagesPath = configuration["UserImage"];
            ImageFileName = configuration["ImageFileName"];
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

        public async Task<Team> GetTeam(int teamId, int userId)
        {
            var user = await UnitOfWork.Users.GetUserAsync(userId);
            var team = await UnitOfWork.Teams.GetTeam(teamId);

            team.Members.ForEach(member =>
            {
                if (File.Exists($"{UserImagesPath}{member.Id}/{ImageFileName}"))
                {
                    var text = File.ReadAllText($"{UserImagesPath}{member.Id}/{ImageFileName}");

                    member.Picture = text;
                }
            });

            if (user.TeamId != team.TeamId && user.IsTeamCaptain == false)
            {
                team.EntryKey = null;
            }

            return team;
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

        public async Task<List<TournamentPlacementOutputDto>> GetTeamPlacements(int teamId)
        {
            var placements = await UnitOfWork.Teams.GetTeamPlacements(teamId);

            return placements.Select(x => new TournamentPlacementOutputDto
            {
                StartDate = x.Tournament.StartDate,
                Placement = x.Placement,
                TournamentName = x.Tournament.Name
            })
            .OrderByDescending(x => x.StartDate)
            .ToList();
        }

        public async Task<bool> DeleteTeamMeber(int teamId, int memberId, int userId)
        {
            var user = await UnitOfWork.Users.GetByIdAsync(userId);
            var team = await UnitOfWork.Teams.FindAsync(teamId);

            if (!user.IsTeamCaptain && !team.Members.Any(x => x.UserName == user.UserName))
            {
                return false;
            }

            var member = team.Members.Find(x => x.Id == memberId);

            if (member == null)
            {
                return false;
            }

            if (member.IsTeamCaptain)
            {
                var newCaptain = team.Members.Find(x => x.Id != memberId);

                if (newCaptain == null)
                {
                    team.Active = false;
                    UnitOfWork.Teams.Update(team);
                    await UnitOfWork.CommitAsync();
                }
            }

            member.TeamId = null;
            member.IsTeamCaptain = false;

            UnitOfWork.Users.Update(member);
            await UnitOfWork.CommitAsync();

            return true;
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
