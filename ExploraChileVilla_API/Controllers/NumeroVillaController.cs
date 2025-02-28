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
    public class NumeroVillaController : ControllerBase
    {

        // Declaraciones de campos privados
        // _logger permite registrar mensajes de log relacionados con las acciones del controlador.
        private readonly ILogger<NumeroVillaController> _logger;
        //private readonly ApplicationDbContext _context; lo remplazamos por nuestro repositorio
        private readonly IVillaRepository _villaRepo;
        private readonly INumeroVillaRepository _numeroVillaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;


        // Inyeccion de dependencias para el logger, que permite registrar eventos y errores en el controlador.
        public NumeroVillaController(
            ILogger<NumeroVillaController> logger, IVillaRepository villaRepo, INumeroVillaRepository numeroVillaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroVillaRepo = numeroVillaRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {

            try
            {
                _logger.LogInformation("Obtener todas Numero villas");
                IEnumerable<NumeroVilla> numerovillaList = await _numeroVillaRepo.ObtenerTodo();

                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numerovillaList);
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




        [HttpGet("{id:int}", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillaById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                // Obtener NumeroVilla sin incluir relaciones
                var numeroVilla = await _numeroVillaRepo.Obtener(v => v.VillaNro == id);

                if (numeroVilla == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<NumeroVillaDto>(numeroVilla);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExistoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.statusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)_response.statusCode, _response);
            }
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNumeroVilla(NumeroVillaCreateDto numeroVillaCreteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar que el Id de la villa existe
                if (await _numeroVillaRepo.Obtener(v => v.VillaNro == numeroVillaCreteDto.VillaNro) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero de villa ya existe");
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Obtener(v => v.Id == numeroVillaCreteDto.VillaId)==null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Ide de la villa no existe!");
                    return BadRequest(ModelState);
                }


                if (numeroVillaCreteDto == null)
                {
                    return BadRequest("El DTO no puede ser nulo");
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(numeroVillaCreteDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _numeroVillaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNro }, modelo);
            }
            catch (Exception ex)
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
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {

            try
            {
                if (id == 0)
                {

                    _response.IsExistoso = false;
                    _response.statusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var numeroVilla = await _numeroVillaRepo.Obtener(x => x.VillaNro == id);

                if (numeroVilla == null)
                {
                    _response.IsExistoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _numeroVillaRepo.Remover(numeroVilla);

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
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto numVillaUpdateDto)
        {
            if (numVillaUpdateDto == null || id != numVillaUpdateDto.VillaNro)
            {
                _response.IsExistoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            // Comprobamos si el NumeroVilla existe en la base de datos
            if(await _numeroVillaRepo.Obtener(x => x.VillaId == numVillaUpdateDto.VillaId) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de la villa no existe.");
                return BadRequest(_response);
            }

            // Mapear el DTO al modelo de NumeroVilla
            var modelo = _mapper.Map<NumeroVilla>(numVillaUpdateDto);

            // Actualizar la entidad de NumeroVilla
            await _numeroVillaRepo.Actualizar(modelo);
            await _numeroVillaRepo.Guardar();

            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }


    }
}
