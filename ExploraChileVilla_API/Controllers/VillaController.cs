using AutoMapper;
using ExploraChileVilla_API.Data;
using ExploraChileVilla_API.Models;
using ExploraChileVilla_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace ExploraChileVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        // Declaraciones de campos privados
        // _logger permite registrar mensajes de log relacionados con las acciones del controlador.
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        // Inyeccion de dependencias para el logger, que permite registrar eventos y errores en el controlador.
        public VillaController(
            ILogger<VillaController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;   
        }


        [HttpGet]
        public async  Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {

            _logger.LogInformation("Obtener todas las villas");
            IEnumerable<Villa> villaList = await _context.Villas.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }


        [HttpGet("{id:int}",  Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVillaById(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al intentar tarer villa, con el Id"+ id);
                return BadRequest();
            }

            var villa = await _context.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok( _mapper.Map<VillaDto>(villa));
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>>CreateVilla(VillaCreateDto villaCreteDto)
        {
            // Verifica si el modelo es valido.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);// Si no es valido, devuelve un error con los detalles de la validacion.
            }


            if (await _context.Villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == villaCreteDto.Nombre.ToLower()) != null)
            {
                // Si ya existe una villa con el mismo nombre, agrega un error al ModelState.
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState); 
            }

            if (villaCreteDto == null)
            {
                return BadRequest(villaCreteDto);
            }

            // Usamos AutoMapper para mapear el DTO VillaCreateDto a la entidad Villa
            //   // para evitar crear un nuevo modelo y pasar todas las propiedades lo mapeamos
            Villa modelo = _mapper.Map<Villa>(villaCreteDto);

            //recibir un nuevo modelo
            //Villa modelo = new Villa()
            //{
            //    Nombre = villaCreteDto.Nombre,
            //    Descripcion = villaCreteDto.Descripcion,
            //    Detalle = villaCreteDto.Detalle,
            //    Ocupantes = villaCreteDto.Ocupantes,
            //    Tarifa = villaCreteDto.Tarifa,
            //    MetrosCuadrados = villaCreteDto.MetrosCuadrados,
            //    Amenidades = villaCreteDto.Amenidades,
            //    ImagenUrl = villaCreteDto.ImagenUrl
            //};






            await _context.Villas.AddAsync(modelo);
           await _context.SaveChangesAsync();

            // Devuelve una respuesta 201 Created con la nueva villa y un enlace a la ruta "GetVilla" para obtener la villa por su Id.
            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, modelo);

        }



        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  async Task<IActionResult> DeleteVilla(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _context.Villas.FirstOrDefaultAsync(x => x.Id == id);

            if (villa == null) {
             return NotFound();
            };

            _context.Villas.Remove(villa);
            await _context.SaveChangesAsync();
            return NoContent();
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id,[FromBody] VillaUpdateDto villaUpdateDto)
        {


            if (villaUpdateDto == null || id != villaUpdateDto.Id)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);


            // para evitar crear un nuevo modelo y pasar todas las propiedades lo mapeamos
            Villa modelo = _mapper.Map<Villa>(villaUpdateDto);
           

            _context.Villas.Update(modelo);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVillaPatch(int id, JsonPatchDocument<VillaUpdateDto> patchVillaDto)
        {
            if (patchVillaDto == null || id == 0)
            {
                return BadRequest();
            }

            // Se obtiene la entidad original de la base de datos
            var villa = await _context.Villas.FirstOrDefaultAsync(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            // Mapeo de la entidad Villa a un DTO VillaUpdateDto usando AutoMapper
            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

            // Se aplica el parche (modificaciones) al DTO en memoria
            patchVillaDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapeo inverso del DTO actualizado a la entidad Villa
            _mapper.Map(villaDto, villa);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
