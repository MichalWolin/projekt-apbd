using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(c => c.CompanyId);
        builder.Property(c => c.CompanyId).ValueGeneratedOnAdd();
        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Krs).HasMaxLength(10).IsRequired();

        builder.HasData(new List<Company>()
        {
            new Company
            {
                CompanyId = 1,
                Name = "Frugo",
                Krs = "1234567890"
            }
        });
    }
}