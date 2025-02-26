﻿// <auto-generated />
using System;
using ExploraChileVilla_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExploraChileVilla_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250225142206_AgregarBaseDatos")]
    partial class AgregarBaseDatos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExploraChileVilla_API.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidades")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidades = "Piscina, Wi-Fi, Estacionamiento",
                            Descripcion = "Una hermosa villa con vistas panorámicas y piscina privada.",
                            Detalle = "Detalle Mercedes",
                            FechaActualizacion = new DateTime(2025, 2, 25, 11, 22, 5, 875, DateTimeKind.Local).AddTicks(1066),
                            FechaCreacion = new DateTime(2025, 2, 25, 11, 22, 5, 875, DateTimeKind.Local).AddTicks(1023),
                            ImagenUrl = "image_url",
                            MetrosCuadrados = 120,
                            Nombre = "Villa Las Mercedes",
                            Ocupantes = 4,
                            Tarifa = 200.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidades = "Wi-Fi, Aire acondicionado, Jardín",
                            Descripcion = "Villa exclusiva frente al mar, ideal para familias.",
                            Detalle = "Villa con vista al mar",
                            FechaActualizacion = new DateTime(2025, 2, 25, 11, 22, 5, 875, DateTimeKind.Local).AddTicks(1071),
                            FechaCreacion = new DateTime(2025, 2, 25, 11, 22, 5, 875, DateTimeKind.Local).AddTicks(1069),
                            ImagenUrl = "image_url_2",
                            MetrosCuadrados = 150,
                            Nombre = "Villa El Mar",
                            Ocupantes = 2,
                            Tarifa = 300.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
