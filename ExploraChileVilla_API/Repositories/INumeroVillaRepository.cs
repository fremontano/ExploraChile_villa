using ExploraChileVilla_API.Data;
using ExploraChileVilla_API.Models;
using ExploraChileVilla_API.Repositories.IRepositories;

namespace ExploraChileVilla_API.Repositories
{
    public class NumeroVillaRepository : Repository<NumeroVilla>, INumeroVillaRepository
    {

        private readonly ApplicationDbContext _context;

        public NumeroVillaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _context.NumeroVillas.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}
