using ExploraChileVilla_API.Models;

namespace ExploraChileVilla_API.Repositories.IRepositories
{
    public interface INumeroVillaRepository : IRepository<NumeroVilla>
    {

        Task<NumeroVilla> Actualizar(NumeroVilla entidad); 
    }
}
