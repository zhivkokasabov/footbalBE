using Core.Contracts.Request.Teams;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITeamsRepository: IRepository<Team>
    {
        Task<Team> GetUserTeam(int userId);
        Task<List<TournamentMatch>> GetTeamMatches(int teamId);
        Task<List<TournamentPlacement>> GetTeamPlacements(int teamId);
        Task<Team> FindAsync(string entryKey);
        Task<Team> FindAsync(int teamId);
        Task<Team> GetTeam(int teamId);
    }
}
