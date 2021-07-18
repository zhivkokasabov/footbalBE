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
    class TournamentPlacementConfiguration : IEntityTypeConfiguration<TournamentPlacement>
    {
        public void Configure(EntityTypeBuilder<TournamentPlacement> builder)
        {
            builder.Property(x => x.Placement).HasMaxLength(16);
        }
    }
}
