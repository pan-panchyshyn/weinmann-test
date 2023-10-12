﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Weinmann.DataAccess;

#nullable disable

namespace Weinmann.DataAccess.Migrations
{
    [DbContext(typeof(WeinmannDataContext))]
    [Migration("20231012145742_AddEmployee")]
    partial class AddEmployee
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Weinmann.Domain.Models.BusinessLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusinessLocations");
                });

            modelBuilder.Entity("Weinmann.Domain.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessLocationId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessLocationId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Weinmann.Domain.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Weinmann.Domain.Models.EmployeeBusinessLocation", b =>
                {
                    b.Property<int>("BusinessLocationId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("BusinessLocationId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeBusinessLocations");
                });

            modelBuilder.Entity("Weinmann.Domain.Models.Customer", b =>
                {
                    b.HasOne("Weinmann.Domain.Models.BusinessLocation", "BusinessLocation")
                        .WithMany("Customers")
                        .HasForeignKey("BusinessLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessLocation");
                });

            modelBuilder.Entity("Weinmann.Domain.Models.EmployeeBusinessLocation", b =>
                {
                    b.HasOne("Weinmann.Domain.Models.BusinessLocation", "BusinessLocation")
                        .WithMany("EmployeeBusinessLocations")
                        .HasForeignKey("BusinessLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Weinmann.Domain.Models.Employee", "Employee")
                        .WithMany("EmployeeBusinessLocations")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessLocation");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Weinmann.Domain.Models.BusinessLocation", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("EmployeeBusinessLocations");
                });

            modelBuilder.Entity("Weinmann.Domain.Models.Employee", b =>
                {
                    b.Navigation("EmployeeBusinessLocations");
                });
#pragma warning restore 612, 618
        }
    }
}
