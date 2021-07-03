using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datum.Configurations
{
    class PlayerPositionConfiguration : IEntityTypeConfiguration<PlayerPosition>
    {
        public void Configure(EntityTypeBuilder<PlayerPosition> builder)
        {
            builder.Property(x => x.Position)
                .HasMaxLength(32);
        }
    }
}
