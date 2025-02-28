using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExploraChileVilla_API.Models.Dto
{
    public class NumeroVillaCreateDto
    {

        [Required]
        public int VillaNro { get; set; }

        [Required]
        public int VillaId { get; set; }

        public String DetalleEspecial { get; set; }

    }
}
