using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExploraChileVilla_API.Models
{
    public class NumeroVilla
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int VillaNro { get; set; }

        // Relación con Villa (obligatoria)
        [Required]
        public int VillaId { get; set; }

        [ForeignKey("VillaId")]
        public Villa Villa { get; set; }

        public string DetalleEspecial { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}
