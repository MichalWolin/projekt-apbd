using Api.Helpers;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.UserId);
        builder.Property(u => u.Login).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Role).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Salt).IsRequired();
        builder.Property(u => u.RefreshToken).IsRequired();
        builder.Property(u => u.RefreshTokenExpiration).IsRequired();

        var adminPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt("admin");
        var userPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt("user");

        builder.HasData(new List<User>()
        {
            new User
            {
                UserId = 1,
                Login = "admin",
                Password = adminPasswordAndSalt.Item1,
                Role = "admin",
                Salt = adminPasswordAndSalt.Item2,
                RefreshToken = SecurityHelpers.GenerateRefreshToken(),
                RefreshTokenExpiration = DateTime.Today.AddDays(1)
            },
            new User
            {
                UserId = 2,
                Login = "user",
                Password = userPasswordAndSalt.Item1,
                Role = "user",
                Salt = userPasswordAndSalt.Item2,
                RefreshToken = SecurityHelpers.GenerateRefreshToken(),
                RefreshTokenExpiration = DateTime.Today.AddDays(1)
            }
        });
    }
}