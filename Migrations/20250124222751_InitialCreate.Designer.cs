﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonApi.Shared.Data;

#nullable disable

namespace MonApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250124222751_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MonApi.API.Customers.Models.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CustomerId");

                    b.HasIndex("PasswordId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MonApi.API.Passwords.Models.Password", b =>
                {
                    b.Property<string>("PasswordId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NumberTries")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("PasswordId");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("MonApi.API.Customers.Models.Customer", b =>
                {
                    b.HasOne("MonApi.API.Passwords.Models.Password", "Password")
                        .WithMany()
                        .HasForeignKey("PasswordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Password");
                });
#pragma warning restore 612, 618
        }
    }
}
