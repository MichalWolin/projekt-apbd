using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons");

        builder.HasKey(p => p.PersonId);
        builder.Property(p => p.PersonId).ValueGeneratedOnAdd();
        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(30);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(30);
        builder.Property(p => p.Pesel).IsRequired().HasMaxLength(11);

        builder.HasData(new List<Person>()
        {
            new Person
            {
                PersonId = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Pesel = "12345678901"
            }
        });
    }
}