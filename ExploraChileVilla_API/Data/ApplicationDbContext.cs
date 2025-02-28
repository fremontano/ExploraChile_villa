using ExploraChileVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExploraChileVilla_API.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<NumeroVilla> NumeroVillas { get; set; }


        //Llenar registros en la tabla villa
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Nombre = "Villa Las Mercedes",
                    Tarifa = 200.0,
                    Ocupantes = 4,
                    Detalle = "Detalle Mercedes",
                    MetrosCuadrados = 120,
                    ImagenUrl = "image_url",
                    Amenidades = "Piscina, Wi-Fi, Estacionamiento",
                    Descripcion = "Una hermosa villa con vistas panorámicas y piscina privada.",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Nombre = "Villa El Mar",
                    Tarifa = 300.0,
                    Ocupantes = 2,
                    Detalle = "Villa con vista al mar",
                    MetrosCuadrados = 150,
                    ImagenUrl = "image_url_2",
                    Amenidades = "Wi-Fi, Aire acondicionado, Jardín",
                    Descripcion = "Villa exclusiva frente al mar, ideal para familias.",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
            );

        }

    }
}
