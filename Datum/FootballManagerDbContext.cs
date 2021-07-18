using Core.Models;
using Datum.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Datum
{
    public class FootballManagerDbContext: IdentityDbContext<User, Role, int>
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentAccess> TournamentAccesses { get; set; }
        public DbSet<TournamentType> TournamentTypes { get; set; }
        public DbSet<PlayingDays> PlayingDays { get; set; }
        public DbSet<PlayerPosition> PlayerPositions { get; set; }
        public DbSet<UserPosition> UserPositions { get; set; }
        public DbSet<TournamentParticipant> TournamentParticipants { get; set; }
        public DbSet<TournamentMatch> TournamentMatches { get; set; }
        public DbSet<TournamentMatchTeam> TournamentMatchTeams { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TournamentPlacement> TournamentPlacements { get; set; }

        public FootballManagerDbContext(DbContextOptions<FootballManagerDbContext> options)
            : base (options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TournamentConfiguration());
            builder.ApplyConfiguration(new PlayerPositionConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new TeamConfiguration());
            builder.ApplyConfiguration(new TournamentMatchConfiguration());
            builder.ApplyConfiguration(new TournamentMatchTeamConfiguration());
            builder.ApplyConfiguration(new TournamentParticipantConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserPositionConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new TournamentPlacementConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
