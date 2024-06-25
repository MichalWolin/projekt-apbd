using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Configurations;

public class SoftwareConfiguration : IEntityTypeConfiguration<Software>
{
    public void Configure(EntityTypeBuilder<Software> builder)
    {
        builder.ToTable("Software");

        builder.HasKey(s => s.SoftwareId);
        builder.Property(s => s.SoftwareId).ValueGeneratedOnAdd();
        builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
        builder.Property(s => s.Description).HasMaxLength(300).IsRequired();
        builder.Property(s => s.Version).HasMaxLength(20).IsRequired();
        builder.Property(s => s.Category).HasMaxLength(50).IsRequired();
        builder.Property(s => s.Price).HasPrecision(8, 2).IsRequired();

        builder.HasData(new List<Software>()
        {
            new Software
            {
                SoftwareId = 1,
                Name = "Visual Studio",
                Description = "Integrated development environment",
                Version = "2022",
                Category = "Development",
                Price = 100
            },
            new Software
            {
                SoftwareId = 2,
                Name = "Photoshop",
                Description = "Image editing software",
                Version = "2022",
                Category = "Design",
                Price = 200
            },
            new Software
            {
                SoftwareId = 3,
                Name = "Office",
                Description = "Office suite",
                Version = "2021",
                Category = "Office",
                Price = 150
            }
        });
    }
}