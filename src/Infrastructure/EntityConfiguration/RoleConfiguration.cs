using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(b => b.Users)
            .WithOne(b => b.Role)
            .HasForeignKey(b => b.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
