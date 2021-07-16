using Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITournamentMatchesRepository: IRepository<TournamentMatch>
    {
        public Task<List<TournamentMatch>> GetTournamentMatches(int tournamentId);
        public Task<List<TournamentMatch>> GetEliminationTournamentMatches(int tournamentId);
        public IQueryable<TournamentMatch> GetTournamentMatchesQueryable(int tournamentId);
    }
}
