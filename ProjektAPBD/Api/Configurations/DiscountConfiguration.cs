using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts");

        builder.HasKey(d => d.DiscountId);
        builder.Property(d => d.DiscountId).ValueGeneratedOnAdd();
        builder.Property(d => d.Rate).IsRequired();
        builder.Property(d => d.StartDate).IsRequired();
        builder.Property(d => d.EndDate).IsRequired();
        builder.Property(d => d.Description).HasMaxLength(300).IsRequired();

        builder.HasOne(d => d.Software)
            .WithMany(p => p.Discounts)
            .HasForeignKey(d => d.SoftwareId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new List<Discount>()
        {
            new Discount
            {
                DiscountId = 1,
                SoftwareId = 1,
                Rate = 10,
                StartDate = DateTime.Today.AddDays(-30),
                EndDate = DateTime.Today.AddDays(30),
                Description = "Discount for new customers"
            },
            new Discount
            {
                DiscountId = 2,
                SoftwareId = 2,
                Rate = 20,
                StartDate = DateTime.Today.AddDays(-60),
                EndDate = DateTime.Today.AddDays(-30),
                Description = "Discount for new customers"
            },
        });
    }
}