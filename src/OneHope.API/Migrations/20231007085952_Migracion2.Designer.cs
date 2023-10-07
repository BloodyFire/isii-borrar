﻿// <auto-generated />
using System;
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
    [Migration("20231007085952_Migracion2")]
    partial class Migracion2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OneHope.API.Models.Devolucion", b =>
                {
                    b.Property<int>("IdDevolucion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDevolucion"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCompra")
                        .HasColumnType("int");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.HasKey("IdDevolucion");

                    b.ToTable("Devolucion");
                });

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
