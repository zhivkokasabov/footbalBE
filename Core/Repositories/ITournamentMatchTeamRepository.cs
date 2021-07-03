using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITournamentMatchTeamRepository : IRepository<TournamentMatchTeam>
    {
        Task<List<TournamentMatchTeam>> GetTournamentMatchTeamsByTeamId(int teamId);
    }
}
