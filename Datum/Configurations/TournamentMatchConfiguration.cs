using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datum.Configurations
{
    class TournamentMatchConfiguration : IEntityTypeConfiguration<TournamentMatch>
    {
        public void Configure(EntityTypeBuilder<TournamentMatch> builder)
        {
            builder.HasOne(tm => tm.Tournament)
                .WithMany(t => t.TournamentMatches)
                .HasForeignKey(t => t.TournamentId)
                .IsRequired();

            builder.HasMany(tm => tm.TournamentMatchTeams)
                .WithOne(tmt => tmt.TournamentMatch)
                .HasForeignKey(tmt => tmt.TournamentMatchId);
        }
    }
}
