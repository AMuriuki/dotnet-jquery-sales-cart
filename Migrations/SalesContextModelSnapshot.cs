﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sales_invoicing_dotnet.Data;

#nullable disable

namespace sales_invoicing_dotnet.Migrations
{
    [DbContext(typeof(SalesContext))]
    partial class SalesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("sku")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Apple AirPods Pro",
                            Price = 8500.00m,
                            sku = "P1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Xiamo Redmi Buds",
                            Price = 2700.00m,
                            sku = "P10"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Oraimo Wireless Earphone",
                            Price = 3400.00m,
                            sku = "P100"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Netac USB Type-C 128 GB",
                            Price = 1600.00m,
                            sku = "P1000"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Sandisk USB 32GB ",
                            Price = 8500.00m,
                            sku = "P10000"
                        });
                });

            modelBuilder.Entity("Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("SaleProduct", b =>
                {
                    b.Property<int>("SaleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SaleId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("SaleProducts");
                });

            modelBuilder.Entity("SaleProduct", b =>
                {
                    b.HasOne("Product", "Product")
                        .WithMany("SaleProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sale", "Sale")
                        .WithMany("SaleProducts")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.Navigation("SaleProducts");
                });

            modelBuilder.Entity("Sale", b =>
                {
                    b.Navigation("SaleProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
