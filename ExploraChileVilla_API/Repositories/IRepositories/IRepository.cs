using System.Linq.Expressions;

namespace ExploraChileVilla_API.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {

        Task Crear(T entidad); 

        Task<List<T>> ObtenerTodo(Expression<Func<T, bool>> filtro = null);

        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true);

        Task Remover(T entidad);

        Task Guardar();

    }
}


//Repositorio Generico, Puede usarse para cualquier entidad 
// Expresiones Lambda: Permiten consultas dinamicas sin escribir SQL 