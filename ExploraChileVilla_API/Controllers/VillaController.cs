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


        // Inyeccion de dependencias para el logger, que permite registrar eventos y errores en el controlador.
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public  ActionResult<IEnumerable<VillaDto>> GetVillas()
        {

            _logger.LogInformation("Obtener todas las villas");
            return Ok(_context.Villas.ToList());
        }


        [HttpGet("{id:int}",  Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVillaById(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al intentar tarer villa, con el Id"+ id);
                return BadRequest();
            }

            var villa = _context.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok( villa);
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla(VillaDto villaDto)
        {
            // Verifica si el modelo es valido.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);// Si no es valido, devuelve un error con los detalles de la validacion.
            }


            if (_context.Villas.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
            {
                // Si ya existe una villa con el mismo nombre, agrega un error al ModelState.
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState); 
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //recibir un nuevo modelo
            Villa modelo = new Villa()
            {
                Nombre = villaDto.Nombre,
                Descripcion = villaDto.Descripcion,
                Detalle = villaDto.Detalle,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidades = villaDto.Amenidades,
                ImagenUrl = villaDto.ImagenUrl
            };




            _context.Villas.Add(modelo);
            _context.SaveChanges();

            // Devuelve una respuesta 201 Created con la nueva villa y un enlace a la ruta "GetVilla" para obtener la villa por su Id.
            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);

        }



        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }

            var villa = _context.Villas.FirstOrDefault(x => x.Id == id);

            if (villa == null) {
             return NotFound();
            };

            _context.Villas.Remove(villa);
            _context.SaveChanges();
            return NoContent();
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id,[FromBody] VillaDto villaDto)
        {


            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);


            //crear un nuevo modelo y pasarles las propiedades
            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Descripcion = villaDto.Descripcion,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidades = villaDto.Amenidades
            };

            _context.Villas.Update(modelo);
            _context.SaveChanges();

            return NoContent();
        }



        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVillaPatch(int id, JsonPatchDocument<VillaDto> patchVillaDto)
        {
            if (patchVillaDto == null || id == 0)
            {
                return BadRequest();
            }

            // Se obtiene la entidad original de la base de datos
            var villa = _context.Villas.FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            // Se crea un DTO (VillaDto) basado en la entidad obtenida
            VillaDto villaDto = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Descripcion = villa.Descripcion,
                Detalle = villa.Detalle,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                ImagenUrl = villa.ImagenUrl,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidades = villa.Amenidades
            };

            // Se aplica el parche (modificaciones) al DTO en memoria
            patchVillaDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Se actualiza la entidad original con los cambios del DTO
            villa.Nombre = villaDto.Nombre;
            villa.Descripcion = villaDto.Descripcion;
            villa.Detalle = villaDto.Detalle;
            villa.Ocupantes = villaDto.Ocupantes;
            villa.Tarifa = villaDto.Tarifa;
            villa.ImagenUrl = villaDto.ImagenUrl;
            villa.MetrosCuadrados = villaDto.MetrosCuadrados;
            villa.Amenidades = villaDto.Amenidades;

            _context.SaveChanges(); 

            return NoContent();
        }


    }
}
