using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Configurations
{
    class TournamentMatchTeamConfiguration : IEntityTypeConfiguration<TournamentMatchTeam>
    {
        public void Configure(EntityTypeBuilder<TournamentMatchTeam> builder)
        {
            builder.HasOne(tmt => tmt.Team)
                .WithMany(t => t.TournamentMatchTeams)
                .HasForeignKey(tmt => tmt.TeamId);

            builder.HasOne(tmt => tmt.TournamentMatch)
                .WithMany(t => t.TournamentMatchTeams)
                .HasForeignKey(tmt => tmt.TournamentMatchId);
        }
    }
}
