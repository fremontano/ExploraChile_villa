﻿using System.ComponentModel.DataAnnotations;

namespace ExploraChileVilla_API.Models.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string ImagenUrl { get; set; }

        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosCuadrados { get; set; }

        [Required]
        public string Amenidades { get; set; } 
    }
}
