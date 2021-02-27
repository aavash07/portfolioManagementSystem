﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortfolioManagementSystem.Models;

namespace PortfolioManagementSystem.Migrations
{
    [DbContext(typeof(StockDbContext))]
    [Migration("20210226101825_Initial create with date change")]
    partial class Initialcreatewithdatechange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PortfolioManagementSystem.Models.StockDetails", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("StockName")
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("TransactionDate")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TransactionType")
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("StockId");

                    b.ToTable("StockDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
