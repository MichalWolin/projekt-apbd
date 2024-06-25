using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contracts");

        builder.HasKey(c => c.ContractId);
        builder.Property(c => c.ContractId).ValueGeneratedOnAdd();
        builder.Property(c => c.StartDate).IsRequired();
        builder.Property(c => c.EndDate).IsRequired();
        builder.Property(c => c.Price).HasPrecision(8, 2).IsRequired();
        builder.Property(c => c.Paid).HasPrecision(8, 2).IsRequired();
        builder.Property(c => c.SupportEndDate).IsRequired();
        builder.Property(c => c.Signed).HasDefaultValue(false).IsRequired();

        builder.HasOne(c => c.Software)
            .WithMany(p => p.Contracts)
            .HasForeignKey(c => c.SoftwareId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Customer)
            .WithMany(c => c.Contracts)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new List<Contract>()
        {
            new Contract
            {
                ContractId = 1,
                SoftwareId = 1,
                CustomerId = 1,
                StartDate = DateTime.Today.AddDays(3),
                EndDate = DateTime.Today.AddDays(33),
                Price = 100,
                Paid = 100,
                SupportEndDate = DateTime.Today.AddDays(368),
                Signed = true
            }
        });
    }
}