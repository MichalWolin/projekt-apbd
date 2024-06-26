using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.CustomerId);
        builder.Property(c => c.CustomerId).ValueGeneratedOnAdd();
        builder.Property(c => c.Address).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(100).IsRequired();
        builder.Property(c => c.PhoneNumber).HasMaxLength(20).IsRequired();
        builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        builder.HasOne(c => c.Person)
            .WithOne(p => p.Customer)
            .HasForeignKey<Customer>(c => c.PersonId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne(c => c.Company)
            .WithOne(c => c.Customer)
            .HasForeignKey<Customer>(c => c.CompanyId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasData(new List<Customer>()
        {
            new Customer
            {
                CustomerId = 1,
                Address = "ul. Testowa 1, 00-001 Warszawa",
                Email = "jan@kowalski.com",
                PhoneNumber = "123456789",
                PersonId = 1,
                IsDeleted = false
            },
            new Customer
            {
                CustomerId = 2,
                Address = "ul. Testowa 2, 00-002 Warszawa",
                Email = "frugo@frugo.com",
                PhoneNumber = "987654321",
                CompanyId = 1,
                IsDeleted = false
            }
        });
    }
}