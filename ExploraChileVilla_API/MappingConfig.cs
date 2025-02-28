using AutoMapper;
using ExploraChileVilla_API.Models;
using ExploraChileVilla_API.Models.Dto;

namespace ExploraChileVilla_API
{

    // Clase que hereda de Profile de AutoMapper, donde definimos los mapeos
    public class MappingConfig: Profile
    {

        public MappingConfig() 
        {

            // Mapeo entre la entidad Villa y el DTO VillaDto
            // Esto significa que, cuando se necesite convertir una Villa en un VillaDto, se usara este mapeo.
            CreateMap<Villa, VillaDto>();
            // Mapeo entre el DTO VillaDto y la entidad Villa
            // Esto es lo contrario al mapeo anterior, por ejemplo, si queremos convertir un VillaDto en una Villa.
            CreateMap<VillaDto, Villa>();

            // Mapeo entre la entidad Villa y el DTO VillaCreateDto, y viceversa.
            // El método ReverseMap() significa que AutoMapper también generará el mapeo inverso
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();


            CreateMap<NumeroVilla, NumeroVillaDto>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaCreateDto>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaUpdateDto>().ReverseMap();




        }

    }
}
