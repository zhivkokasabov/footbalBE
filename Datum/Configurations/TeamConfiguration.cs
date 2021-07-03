using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datum.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");
            builder.Property(t => t.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(t => t.EntryKey)
                .HasMaxLength(64)
                .IsRequired();

            builder.HasMany(t => t.TournamentMatchTeams)
                .WithOne(tm => tm.Team)
                .HasForeignKey(tm => tm.TeamId);

            builder.HasMany(t => t.TournamentParticipants)
                .WithOne(tp => tp.Team)
                .HasForeignKey(tp => tp.TeamId);
        }
    }
}
