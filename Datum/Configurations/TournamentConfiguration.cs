using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datum.Configurations
{
    public class TournamentConfiguration: IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.Property(t => t.Description)
                .HasMaxLength(2000);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Avenue)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.StartDate)
                .IsRequired();

            builder.Property(t => t.Rules)
                .HasMaxLength(2000);

            builder.Property(t => t.PlayingFields)
                .IsRequired();

            builder.Property(t => t.MatchLength)
                .IsRequired();

            builder.Property(t => t.HalfTimeLength)
                .IsRequired();

            builder.HasMany(t => t.TournamentParticipants)
                .WithOne(tp => tp.Tournament);

            builder.HasMany(t => t.TournamentMatches)
                .WithOne(tm => tm.Tournament);
        }
    }
}
