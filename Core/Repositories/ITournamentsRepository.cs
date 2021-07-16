using Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITournamentsRepository: IRepository<Tournament>
    {
        IQueryable<Tournament> GetTournamentQueryable(int tournamentId);
        IQueryable<Tournament> GetTournamentQueryable();
        Task<List<Tournament>> GetAllTournamentsAsync(int pageSize, int page);
        Task<List<Tournament>> GetUserTournaments(int userId);
        Task<List<Tournament>> GetTournamentWithUserParticipation(int userId);
        Task<Tournament> GetTournamentById(int tournamentId);
        Task<List<TournamentMatch>> GetTournamentMatchesBySequenceId(int teamSequenceId, int tournamentId);
        Task<int> GetLastRoundNumber(int tournamentId);
    }
}
