using Core.contracts.response;
using Core.contracts.Response;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITournamentsService
    {
        Task<Tournament> CreateTournament(Tournament tournament, int userId);
        Task<List<Tournament>> GetAllTournaments(int pageSize, int page);
        Task<Tournament> GetTournamentById(int tournamentId);
        Task<List<Tournament>> GetUserTournaments(int userId, string role);
        Task<List<TournamentParticipant>> GetTournamentParticipants(int tournamentId);
        Task<bool> GetUserIsAllowedToParticipate(int tournamentId, int userId);
        Task<List<ErrorModel>> JoinTournament(int tournamentId, int teamId);
        Task<IEnumerable<IGrouping<int, TournamentMatch>>> GetTournamentMatches(int tournamentId);
    }
}
