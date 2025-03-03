﻿using System.ComponentModel.DataAnnotations;

namespace ExploraChileVilla_API.Models.Dto
{
    public class VillaCreateDto
    {
        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public string Descripcion { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidades { get; set; } 
    }
}
