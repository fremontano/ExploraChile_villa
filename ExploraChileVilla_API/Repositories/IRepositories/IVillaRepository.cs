using ExploraChileVilla_API.Models;

namespace ExploraChileVilla_API.Repositories.IRepositories
{
    public interface IVillaRepository :IRepository<Villa>
    {

        Task<Villa> Actualizar(Villa entidad); 
    }
}
