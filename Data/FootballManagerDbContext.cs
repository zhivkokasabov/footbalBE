using Core.Models;
using Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class FootballManagerDbContext: DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentAccess> TournamentAccesses { get; set; }
        public DbSet<TournamentType> TournamentTypes { get; set; }
        public DbSet<PlayingDays> PlayingDays { get; set; }
        public DbSet<User> Users { get; set; }

        public FootballManagerDbContext(DbContextOptions<FootballManagerDbContext> options)
            : base (options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TournamentConfiguration());
        }
    }
}
