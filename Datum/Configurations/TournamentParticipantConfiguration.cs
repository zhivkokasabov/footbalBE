using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Datum.Configurations
{
    public class TournamentParticipantConfiguration: IEntityTypeConfiguration<TournamentParticipant>
    {
        public void Configure(EntityTypeBuilder<TournamentParticipant> builder)
        {
            builder.HasOne(tp => tp.Tournament)
                .WithMany(t => t.TournamentParticipants)
                .HasForeignKey(t => t.TournamentId)
                .IsRequired();

            builder.HasOne(tp => tp.Team)
                .WithMany(tp => tp.TournamentParticipants)
                .HasForeignKey(tp => tp.TeamId);
        }
    }
}
