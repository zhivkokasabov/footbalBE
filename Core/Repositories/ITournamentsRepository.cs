using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITournamentsRepository: IRepository<Tournament>
    {
        Task<List<Tournament>> GetAllTournamentsAsync(int pageSize, int page);
        Task<List<Tournament>> GetUserTournaments(int userId);
        Task<List<Tournament>> GetTournamentWithUserParticipation(int userId);
        Task<Tournament> GetTournamentById(int tournamentId);
        Task<List<TournamentMatch>> GetTournamentMatchesBySequenceId(int teamSequenceId, int tournamentId);
    }
}
