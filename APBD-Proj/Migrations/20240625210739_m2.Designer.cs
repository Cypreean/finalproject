﻿// <auto-generated />
using System;
using APBD_Proj.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APBD_Proj.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240625210739_m2")]
    partial class m2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.5.24306.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APBD_Proj.Models.Companies", b =>
                {
                    b.Property<int>("IdCompany")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CompanyID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCompany"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Address");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<int>("KRS")
                        .HasMaxLength(14)
                        .HasColumnType("int")
                        .HasColumnName("KRS");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int")
                        .HasColumnName("PhoneNumber");

                    b.HasKey("IdCompany");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("APBD_Proj.Models.Contracts", b =>
                {
                    b.Property<int>("IdContract")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ContractID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdContract"));

                    b.Property<int>("DiscountsIdDiscount")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int")
                        .HasColumnName("CompanyID");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<int?>("IdDiscount")
                        .HasColumnType("int")
                        .HasColumnName("DiscountID");

                    b.Property<int>("IdSoftware")
                        .HasColumnType("int")
                        .HasColumnName("SoftwareID");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit")
                        .HasColumnName("IsPaid");

                    b.Property<bool>("IsSigned")
                        .HasColumnType("bit")
                        .HasColumnName("IsSigned");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TotalAmount");

                    b.HasKey("IdContract");

                    b.HasIndex("DiscountsIdDiscount");

                    b.HasIndex("IdCompany");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdSoftware");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("APBD_Proj.Models.Customers", b =>
                {
                    b.Property<int>("IdCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCustomer"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FirstName");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasColumnName("LastName");

                    b.Property<int>("Pesel")
                        .HasMaxLength(11)
                        .HasColumnType("int")
                        .HasColumnName("Pesel");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int")
                        .HasColumnName("PhoneNumber");

                    b.HasKey("IdCustomer");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("APBD_Proj.Models.Discounts", b =>
                {
                    b.Property<int>("IdDiscount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DiscountID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDiscount"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Percentage");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate");

                    b.HasKey("IdDiscount");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("APBD_Proj.Models.Payments", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Amount");

                    b.Property<int>("IdContract")
                        .HasColumnType("int")
                        .HasColumnName("ContractID");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("PaymentDate");

                    b.HasKey("PaymentId");

                    b.HasIndex("IdContract");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("APBD_Proj.Models.Software", b =>
                {
                    b.Property<int>("IdSoftware")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SoftwareID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSoftware"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Category");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Version");

                    b.HasKey("IdSoftware");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("APBD_Proj.Models.Subscriptions", b =>
                {
                    b.Property<int>("IdSubscription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SubscriptionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSubscription"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int")
                        .HasColumnName("CompanyID");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<int>("IdSoftware")
                        .HasColumnType("int")
                        .HasColumnName("SoftwareID");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.Property<string>("RenewalPeriod")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("RenewalPeriod");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate");

                    b.HasKey("IdSubscription");

                    b.HasIndex("IdCompany");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdSoftware");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("APBD_Proj.Models.Contracts", b =>
                {
                    b.HasOne("APBD_Proj.Models.Discounts", "Discounts")
                        .WithMany("Contracts")
                        .HasForeignKey("DiscountsIdDiscount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APBD_Proj.Models.Companies", "Companies")
                        .WithMany("Contracts")
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APBD_Proj.Models.Customers", "Customers")
                        .WithMany("Contracts")
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APBD_Proj.Models.Software", "Software")
                        .WithMany("Contracts")
                        .HasForeignKey("IdSoftware")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Companies");

                    b.Navigation("Customers");

                    b.Navigation("Discounts");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("APBD_Proj.Models.Payments", b =>
                {
                    b.HasOne("APBD_Proj.Models.Contracts", "Contracts")
                        .WithMany("Payments")
                        .HasForeignKey("IdContract")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("APBD_Proj.Models.Subscriptions", b =>
                {
                    b.HasOne("APBD_Proj.Models.Companies", "Companies")
                        .WithMany("Subscriptions")
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APBD_Proj.Models.Customers", "Customers")
                        .WithMany("Subscriptions")
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APBD_Proj.Models.Software", "Software")
                        .WithMany("Subscriptions")
                        .HasForeignKey("IdSoftware")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Companies");

                    b.Navigation("Customers");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("APBD_Proj.Models.Companies", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("APBD_Proj.Models.Contracts", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("APBD_Proj.Models.Customers", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("APBD_Proj.Models.Discounts", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("APBD_Proj.Models.Software", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
