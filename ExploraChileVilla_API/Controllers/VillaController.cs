using AutoMapper;
using ExploraChileVilla_API.Data;
using ExploraChileVilla_API.Models;
using ExploraChileVilla_API.Models.Dto;
using ExploraChileVilla_API.Repositories.IRepositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;



namespace ExploraChileVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        // Declaraciones de campos privados
        // _logger permite registrar mensajes de log relacionados con las acciones del controlador.
        private readonly ILogger<VillaController> _logger;
        //private readonly ApplicationDbContext _context; lo remplazamos por nuestro repositorio
        private readonly IVillaRepository _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;


        // Inyeccion de dependencias para el logger, que permite registrar eventos y errores en el controlador.
        public VillaController(
            ILogger<VillaController> logger, IVillaRepository villaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {

            try
            {
                _logger.LogInformation("Obtener todas las villas");
                IEnumerable<Villa> villaList = await _villaRepo.ObtenerTodo();

                _response.Resultado = _mapper.Map<IEnumerable<VillaDto>>(villaList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExistoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al intentar tarer villa, con el Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villa = await _villaRepo.Obtener(v => v.Id == id);

                if (villa == null)
                {
                    _response.statusCode=HttpStatusCode.NotFound;
                    _response.IsExistoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<VillaDto>(villa);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExistoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla(VillaCreateDto villaCreteDto)
        {
            try
            {
                // Verifica si el modelo es valido.
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                if (await _villaRepo.Obtener(v => v.Nombre.ToLower() == villaCreteDto.Nombre.ToLower()) != null)
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

                //antes de guardar un nuevo registro, actualizamos la fecha de creacion y actualizacion
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _villaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                // Devuelve una respuesta 201 Created con la nueva villa y un enlace a la ruta "GetVilla" para obtener la villa por su Id.
                return CreatedAtRoute("GetVilla", new { id = modelo.Id }, modelo);
            }
            catch(Exception ex)
            {
                _response.IsExistoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }



        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {

            try
            {
                if (id == 0)
                {

                    _response.IsExistoso = false;
                    _response.statusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villa = await _villaRepo.Obtener(x => x.Id == id);

                if (villa == null)
                {
                    _response.IsExistoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return NotFound(_response);
                };

                await _villaRepo.Remover(villa);

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsExistoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);

        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {


            if (villaUpdateDto == null || id != villaUpdateDto.Id)
            {
                _response.IsExistoso=false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);


            // para evitar crear un nuevo modelo y pasar todas las propiedades lo mapeamos
            Villa modelo = _mapper.Map<Villa>(villaUpdateDto);


            await _villaRepo.Actualizar(modelo);
            await _villaRepo.Guardar();
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
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
            var villa = await _villaRepo.Obtener(x => x.Id == id, tracked: false);

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
            Villa modelo = _mapper.Map<Villa>(villaDto);

            await _villaRepo.Actualizar(modelo);
            await _villaRepo.Guardar();

            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }


    }
}
