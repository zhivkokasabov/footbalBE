using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITournamentParticipantsRepository: IRepository<TournamentParticipant>
    {
        Task<List<TournamentParticipant>> GetTournamentParticipants(int tournamentId);
        Task<TournamentParticipant> FindUnassignedParticipant(int tournamentId);
        Task<int> GetNumberOfEnrolledTeams(int tournamentId);
    }
}
