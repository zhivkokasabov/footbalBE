using System;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ITournamentsRepository Tournaments { get; }
        IUsersRepository Users { get; }
        IRolesRepository Roles { get; }
        IPlayerPositionsRepository PlayerPositions { get; }
        ITournamentTypesRepository TournamentTypes { get; }
        ITournamentAccessesRepository TournamentAccesses { get; }
        ITournamentParticipantsRepository TournamentParticipants { get; }
        ITournamentMatchesRepository TournamentMatches { get; }
        ITeamsRepository Teams { get; }
        ITournamentPlacementRepository TournamentPlacements { get; }
        INotificationRepository Notifications { get; }
        ITournamentMatchTeamRepository TournamentMatchTeams { get; }
        Task<int> CommitAsync();
    }
}
