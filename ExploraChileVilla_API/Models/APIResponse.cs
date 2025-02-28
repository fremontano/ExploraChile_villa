using System.Net;

namespace ExploraChileVilla_API.Models
{
    // Clase que encapsula la respuesta que la API devolvera al cliente
    public class APIResponse
    {

        // Codigo de estado HTTP de la respuesta, como 200 OK, 400 BadRequest, 404 NotFound, etc.
        public HttpStatusCode statusCode {  get; set; }
        public bool IsExistoso { get; set; }  = true;

        // Lista de mensajes de error que puede devolver
        public List<String> ErrorMessages { get; set; }

        // Resultado de la operacion, puede contener cualquier tipo de datos, dependiendo de la operacion realizada
        // Si la operacion fue exitosa, esta propiedad puede contener el objeto o lista de datos solicitados
        public object Resultado { get; set; }
    }
}
