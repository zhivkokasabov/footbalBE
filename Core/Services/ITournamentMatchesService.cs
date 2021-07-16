using Core.Contracts.Request.Tournaments;
using Core.InternalObjects;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITournamentMatchesService
    {
        Task<UpsertTournamentMatchModel> UpsertTournamentMatch(MatchResultDto result, int matchId);
    }
}
