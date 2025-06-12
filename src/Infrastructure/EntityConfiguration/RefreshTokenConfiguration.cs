using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshToken");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Token)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.ExpiresAt)
            .IsRequired();

        builder.Property(b => b.IsRevoked)
            .IsRequired();
    }
}
