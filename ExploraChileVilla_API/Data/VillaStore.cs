using ExploraChileVilla_API.Models.Dto;

namespace ExploraChileVilla_API.Data
{
    public class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
    {
        new VillaDto
        {
            Id = 1,
            Nombre = "Villa piscina",
            Descripcion = "Una villa con piscina privada.",
            Ocupantes = 2, 
            MetrosCuadrados = 150 
        },
        new VillaDto
        {
            Id = 2,
            Nombre = "Villa Playa",
            Descripcion = "Una villa frente al mar.",
            Ocupantes = 8,  
            MetrosCuadrados = 180  
        }
    };
    }

}
