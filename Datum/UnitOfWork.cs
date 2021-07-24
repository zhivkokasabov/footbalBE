using Core.Models;
using Core.Repositories;
using Datum;
using Datum.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Datum
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly FootballManagerDbContext Context;
        private TournamentRepository TournamentRepository;
        private UserRepository UserRepository;
        private RoleRepository RoleRepository;
        private PlayerPositionRepository PlayerPositionRepository;
        private TournamentTypesRepository TournamentTypesRepository;
        private TournamentAccessesRepository TournamentAccessesRepository;
        private TournamentParticipantsRepository TournamentParticipantsRepository;
        private TournamentMatchesRepository TournamentMatchesRepository;
        private TeamsRepository TeamsRepository;
        private TournamentPlacementRepository TournamentPlacementRepository;
        private NotificationRepository NotificationRepository;

        public UnitOfWork(FootballManagerDbContext context)
        {
            Context = context;
        }

        public ITournamentsRepository Tournaments
            => TournamentRepository = TournamentRepository ?? new TournamentRepository(Context);

        public IUsersRepository Users
            => UserRepository = UserRepository ?? new UserRepository(Context);

        public IRolesRepository Roles
            => RoleRepository = RoleRepository ?? new RoleRepository(Context);

        public IPlayerPositionsRepository PlayerPositions
            => PlayerPositionRepository = PlayerPositionRepository ?? new PlayerPositionRepository(Context);

        public ITournamentTypesRepository TournamentTypes
            => TournamentTypesRepository = TournamentTypesRepository ?? new TournamentTypesRepository(Context);

        public ITournamentAccessesRepository TournamentAccesses
            => TournamentAccessesRepository = TournamentAccessesRepository ?? new TournamentAccessesRepository(Context);

        public ITournamentParticipantsRepository TournamentParticipants
            => TournamentParticipantsRepository = TournamentParticipantsRepository ?? new TournamentParticipantsRepository(Context);

        public ITournamentMatchesRepository TournamentMatches
            => TournamentMatchesRepository = TournamentMatchesRepository ?? new TournamentMatchesRepository(Context);

        public ITeamsRepository Teams
            => TeamsRepository = TeamsRepository ?? new TeamsRepository(Context);

        public ITournamentPlacementRepository TournamentPlacements
            => TournamentPlacementRepository = TournamentPlacementRepository ?? new TournamentPlacementRepository(Context);

        public INotificationRepository Notifications
    => NotificationRepository = NotificationRepository ?? new NotificationRepository(Context);

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
