using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mokes.API.Models;

namespace Mokes.API.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Username).IsUnique();

            builder
                .HasMany(u => u.Entries)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            builder
                .HasMany(u => u.Tags)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);
        }
    }
}
