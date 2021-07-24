using Core.contracts.response;
using Core.contracts.Response;
using Core.Contracts.Response;
using Core.Contracts.Response.Tournaments;
using Core.InternalObjects;
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
        Task<PagedResult<TournamentOutputDto>> GetAllTournaments(int pageSize, int page);
        Task<TournamentOutputDto> GetTournamentById(int tournamentId, int userId);
        Task<List<Tournament>> GetUserTournaments(int userId, string role);
        Task<List<TournamentParticipant>> GetTournamentParticipants(int tournamentId);
        Task<bool> GetUserIsAllowedToParticipate(int tournamentId, int userId);
        Task<List<ErrorModel>> JoinTournament(int tournamentId, int userId);
        Task<List<ErrorModel>> RequestToJoinTournament(Notification notification, int tournamentId, int userId);
        Task<IEnumerable<IGrouping<int, TournamentMatchOutputDto>>> GetTournamentMatches(int tournamentId, int userId);
        Task<bool> GetCanProceedToEliminations(int tournamentId, int userId);
        Task<ProceedToEliminationsModel> ProceedToEliminations(int tournamentId, int userId);
        Task<CloseTournamentModel> CloseTournament(int tournamentId, int userId);
    }
}
