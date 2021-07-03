using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class TournamentConfiguration: IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Description)
                .HasMaxLength(2000);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Avenue)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.TeamsCount)
                .IsRequired();

            builder.Property(t => t.StartDate)
                .IsRequired();

            builder.Property(t => t.Rules)
                .HasMaxLength(2000);

            builder.Property(t => t.PlayingFieldsCount)
                .IsRequired();

            builder.Property(t => t.MatchLength)
                .IsRequired();

            builder.Property(t => t.HalfTimeLength)
                .IsRequired();

            builder.HasOne(t => t.PlayingDays)
                .WithMany(pd => pd.Tournaments)
                .HasForeignKey(t => t.PlayingDaysId)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(u => u.Tournaments)
                .HasForeignKey(t => t.UserId)
                .IsRequired();

            builder.HasOne(t => t.TournamentAccess)
                .WithMany(ta => ta.Tournaments)
                .HasForeignKey(t => t.TournamentAccessId)
                .IsRequired();

            builder.HasOne(t => t.TournamentType)
                .WithMany(tt => tt.Tournaments)
                .HasForeignKey(t => t.TournamentTypeId)
                .IsRequired();
        }
    }
}
