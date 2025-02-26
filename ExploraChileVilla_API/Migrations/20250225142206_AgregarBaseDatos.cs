using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExploraChileVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarBaseDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarifa = table.Column<double>(type: "float", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ocupantes = table.Column<int>(type: "int", nullable: false),
                    MetrosCuadrados = table.Column<int>(type: "int", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amenidades = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidades", "Descripcion", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "Piscina, Wi-Fi, Estacionamiento", "Una hermosa villa con vistas panorámicas y piscina privada.", "Detalle Mercedes", new DateTime(2025, 2, 25, 11, 22, 5, 875, DateTimeKind.Local).AddTicks(1066), new DateTime(2025, 2, 25, 11, 22, 5, 875, DateTimeKind.Local).AddTicks(1023), "image_url", 120, "Villa Las Mercedes", 4, 200.0 },
                    { 2, "Wi-Fi, Aire acondicionado, Jardín", "Villa exclusiva frente al mar, ideal para familias.", "Villa con vista al mar", new DateTime(2025, 2, 25, 11, 22, 5, 875, DateTimeKind.Local).AddTicks(1071), new DateTime(2025, 2, 25, 11, 22, 5, 875, DateTimeKind.Local).AddTicks(1069), "image_url_2", 150, "Villa El Mar", 2, 300.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}
