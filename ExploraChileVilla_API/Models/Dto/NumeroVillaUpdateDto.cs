using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExploraChileVilla_API.Models.Dto
{
    public class NumeroVillaUpdateDto
    {

        [Required]
        public int VillaNro { get; set; }

        [Required]
        public int VillaId { get; set; }

        public String DetalleEspecial { get; set; }

    }
}
