﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineStoreApp.Settings;

#nullable disable

namespace OnlineStoreApp.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20230530124310_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineStoreApp.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Item");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 5,
                            Name = "Test",
                            OrderId = 1,
                            Price = 100.0,
                            ProductId = 1
                        });
                });

            modelBuilder.Entity("OnlineStoreApp.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCancelled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<double>("OrderPrice")
                        .HasColumnType("float");

                    b.Property<DateTime>("OrderTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 5, 30, 14, 43, 9, 807, DateTimeKind.Local).AddTicks(692));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DeliveryAddress = "123",
                            DeliveryTime = new DateTime(2023, 5, 30, 15, 56, 9, 807, DateTimeKind.Local).AddTicks(3884),
                            IsCancelled = false,
                            OrderPrice = 500.0,
                            OrderTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 3
                        });
                });

            modelBuilder.Entity("OnlineStoreApp.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 10,
                            Description = "123",
                            Name = "Test",
                            Price = 100.0,
                            SellerId = 2
                        });
                });

            modelBuilder.Entity("OnlineStoreApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VerificationStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Admin 123",
                            Birthday = new DateTime(1978, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@luka.com",
                            FullName = "Admin Admin",
                            Password = "$2a$11$Eg3bhCAUENNtHSxidNmW3en2IsHocsnLwqw6J4DVx6IDR02kufnwe",
                            Type = "Administrator",
                            Username = "admin",
                            VerificationStatus = "Waiting"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Seller 123",
                            Birthday = new DateTime(1978, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "luka.ciric2000@gmail.com",
                            FullName = "Seller Seller",
                            Password = "$2a$11$WbkM.ijkpW.kvd4fgkTtRe/MTJxiqTLZqNWwRaIfmef4IeonU77vC",
                            Type = "Seller",
                            Username = "seller",
                            VerificationStatus = "Waiting"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Buyer 123",
                            Birthday = new DateTime(1978, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "buyer@luka.com",
                            FullName = "Buyer Buyer",
                            Password = "$2a$11$W35YyxpraMFOcu5bx7Bu4OWN68NeHY7f.FDvuZRLsuKSr3eivlK0y",
                            Type = "Buyer",
                            Username = "buyer",
                            VerificationStatus = "Waiting"
                        });
                });

            modelBuilder.Entity("OnlineStoreApp.Models.Item", b =>
                {
                    b.HasOne("OnlineStoreApp.Models.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OnlineStoreApp.Models.Order", b =>
                {
                    b.HasOne("OnlineStoreApp.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineStoreApp.Models.Product", b =>
                {
                    b.HasOne("OnlineStoreApp.Models.User", "Seller")
                        .WithMany("Products")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("OnlineStoreApp.Models.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("OnlineStoreApp.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
