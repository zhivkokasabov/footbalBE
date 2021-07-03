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
    public class UserPositionConfiguration : IEntityTypeConfiguration<UserPosition>
    {
        public void Configure(EntityTypeBuilder<UserPosition> builder)
        {
            builder.HasKey(x => new { x.UserId, x.PlayerPositionId });

            builder.HasOne(x => x.User)
                .WithMany(u => u.UserPositions)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.PlayerPosition)
                .WithMany(u => u.UserPositions)
                .HasForeignKey(x => x.PlayerPositionId)
                .IsRequired();
        }
    }
}
