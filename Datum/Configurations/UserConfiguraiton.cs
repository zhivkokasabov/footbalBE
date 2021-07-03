using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datum.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Picture)
                .HasMaxLength(4000);

            builder.Property(x => x.Nickname)
                .HasMaxLength(64);

            builder.Property(x => x.FirstName)
                .HasMaxLength(64);

            builder.Property(x => x.LastName)
                .HasMaxLength(64);

            builder.HasOne(x => x.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(x => x.TeamId);

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.HasMany(x => x.UserPositions)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.UserId);
        }
    }
}
