﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OneHope.API.Models;

#nullable disable

namespace OneHope.API.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20231004105418_OneHope")]
    partial class OneHope
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OneHope.API.Models.Portatil", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<float>("PrecioAlquiler")
                        .HasColumnType("real");

                    b.Property<float>("PrecioCompra")
                        .HasColumnType("real");

                    b.Property<int>("PrecioCoste")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("StockAlquiler")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Portatiles");
                });
#pragma warning restore 612, 618
        }
    }
}
