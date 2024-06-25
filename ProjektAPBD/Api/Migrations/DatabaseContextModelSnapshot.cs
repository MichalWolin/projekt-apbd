﻿// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("Krs")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies", (string)null);

                    b.HasData(
                        new
                        {
                            CompanyId = 1,
                            Krs = "1234567890",
                            Name = "Frugo"
                        });
                });

            modelBuilder.Entity("Api.Models.Contract", b =>
                {
                    b.Property<int>("ContractId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContractId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Paid")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal>("Price")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<bool>("Signed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SupportEndDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ContractId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SoftwareId");

                    b.ToTable("Contracts", (string)null);

                    b.HasData(
                        new
                        {
                            ContractId = 1,
                            CustomerId = 1,
                            EndDate = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Local),
                            Paid = 100m,
                            Price = 100m,
                            Signed = true,
                            SoftwareId = 1,
                            StartDate = new DateTime(2024, 6, 28, 0, 0, 0, 0, DateTimeKind.Local),
                            SupportEndDate = new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Local)
                        });
                });

            modelBuilder.Entity("Api.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("CustomerId");

                    b.HasIndex("CompanyId")
                        .IsUnique()
                        .HasFilter("[CompanyId] IS NOT NULL");

                    b.HasIndex("PersonId")
                        .IsUnique()
                        .HasFilter("[PersonId] IS NOT NULL");

                    b.ToTable("Customers", (string)null);

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Address = "ul. Testowa 1, 00-001 Warszawa",
                            Email = "jan@kowalski.com",
                            PersonId = 1,
                            PhoneNumber = "123456789"
                        },
                        new
                        {
                            CustomerId = 2,
                            Address = "ul. Testowa 2, 00-002 Warszawa",
                            CompanyId = 1,
                            Email = "frugo@frugo.com",
                            PhoneNumber = "987654321"
                        });
                });

            modelBuilder.Entity("Api.Models.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscountId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DiscountId");

                    b.HasIndex("SoftwareId");

                    b.ToTable("Discounts", (string)null);

                    b.HasData(
                        new
                        {
                            DiscountId = 1,
                            Description = "Discount for new customers",
                            EndDate = new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Local),
                            Rate = 10,
                            SoftwareId = 1,
                            StartDate = new DateTime(2024, 5, 26, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            DiscountId = 2,
                            Description = "Discount for new customers",
                            EndDate = new DateTime(2024, 5, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            Rate = 20,
                            SoftwareId = 2,
                            StartDate = new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Local)
                        });
                });

            modelBuilder.Entity("Api.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons", (string)null);

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            FirstName = "Jan",
                            LastName = "Kowalski",
                            Pesel = "12345678901"
                        });
                });

            modelBuilder.Entity("Api.Models.Software", b =>
                {
                    b.Property<int>("SoftwareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoftwareId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("SoftwareId");

                    b.ToTable("Software", (string)null);

                    b.HasData(
                        new
                        {
                            SoftwareId = 1,
                            Category = "Development",
                            Description = "Integrated development environment",
                            Name = "Visual Studio",
                            Price = 100m,
                            Version = "2022"
                        },
                        new
                        {
                            SoftwareId = 2,
                            Category = "Design",
                            Description = "Image editing software",
                            Name = "Photoshop",
                            Price = 200m,
                            Version = "2022"
                        },
                        new
                        {
                            SoftwareId = 3,
                            Category = "Office",
                            Description = "Office suite",
                            Name = "Office",
                            Price = 150m,
                            Version = "2021"
                        });
                });

            modelBuilder.Entity("Api.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Login = "admin",
                            Password = "kQmYM7XBosOKQRJEidga97V1oOkEVY6+k1VnqncMzyo=",
                            RefreshToken = "E1VK+RnDKK7rTA7UYAbDBbrEZ7O4+XUFbx8Vujk+ld4=",
                            RefreshTokenExpiration = new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            Role = "admin",
                            Salt = "l0LQzgdl4y9jOe7GdE0WlQ=="
                        },
                        new
                        {
                            UserId = 2,
                            Login = "user",
                            Password = "t+TDMxNWmj4mTDqFgYjbcZNVtSugVLQb0kAGNRy6H6I=",
                            RefreshToken = "SWuhQB6L59ORKuuaXuIIdH5eavs0bWQiRjsWleMz3HI=",
                            RefreshTokenExpiration = new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            Role = "user",
                            Salt = "zBSi5J0QVgifBwQY+hyO2A=="
                        });
                });

            modelBuilder.Entity("Api.Models.Contract", b =>
                {
                    b.HasOne("Api.Models.Customer", "Customer")
                        .WithMany("Contracts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Software", "Software")
                        .WithMany("Contracts")
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("Api.Models.Customer", b =>
                {
                    b.HasOne("Api.Models.Company", "Company")
                        .WithOne("Customer")
                        .HasForeignKey("Api.Models.Customer", "CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Api.Models.Person", "Person")
                        .WithOne("Customer")
                        .HasForeignKey("Api.Models.Customer", "PersonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Company");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Api.Models.Discount", b =>
                {
                    b.HasOne("Api.Models.Software", "Software")
                        .WithMany("Discounts")
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Software");
                });

            modelBuilder.Entity("Api.Models.Company", b =>
                {
                    b.Navigation("Customer")
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.Customer", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("Api.Models.Person", b =>
                {
                    b.Navigation("Customer")
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.Software", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Discounts");
                });
#pragma warning restore 612, 618
        }
    }
}
