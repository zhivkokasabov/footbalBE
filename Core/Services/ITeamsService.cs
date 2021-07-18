using Core.Contracts.Request.Teams;
using Core.Contracts.Response.Teams;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITeamsService
    {
        Task<Team> CreateTeam(TeamDto team, int userId);
        Task<Team> GetUserTeam(int userId);
        Task<Team> AddUserToTeam(int userId, string entryKey);
        Task<List<TournamentMatch>> GetTeamMatches(int teamId);
        Task<List<TournamentPlacementOutputDto>> GetTeamPlacements(int teamId);
        Task<Team> GetTeam(int teamId);
    }
}
