using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contact");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(c => c.Email)
            .HasMaxLength(100);

        builder.Property(c => c.Address)
            .HasMaxLength(200);
    }
}
