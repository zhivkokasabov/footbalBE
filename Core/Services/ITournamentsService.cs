using Core.contracts.response;
using Core.contracts.Response;
using Core.Contracts.Response.Tournaments;
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
        Task<TournamentOutputDto> GetTournamentById(int tournamentId, int userId);
        Task<List<Tournament>> GetUserTournaments(int userId, string role);
        Task<List<TournamentParticipant>> GetTournamentParticipants(int tournamentId);
        Task<bool> GetUserIsAllowedToParticipate(int tournamentId, int userId);
        Task<List<ErrorModel>> JoinTournament(int tournamentId, int userId);
        Task<IEnumerable<IGrouping<int, TournamentMatchOutputDto>>> GetTournamentMatches(int tournamentId);
        Task<bool> GetCanProceedToEliminations(int tournamentId, int userId);
        Task<ErrorResponse> ProceedToEliminations(int tournamentId, int userId);
        Task<ErrorResponse> CloseTournament(int tournamentId, int userId);
    }
}
