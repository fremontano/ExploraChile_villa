using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExploraChileVilla_API.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public String Detalle { get; set; }
        public double Tarifa { get; set; }
        public string Descripcion { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }

        // Lista de Amenidades
        public string Amenidades { get; set; }

        // FechaCreacion y FechaActualizacion, Pueden omitirse si no son necesarias para el cliente, ya que son datos internos del servidor.
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

    }
}
