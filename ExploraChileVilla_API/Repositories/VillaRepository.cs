using ExploraChileVilla_API.Data;
using ExploraChileVilla_API.Models;
using ExploraChileVilla_API.Repositories.IRepositories;

namespace ExploraChileVilla_API.Repositories
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {

        private readonly ApplicationDbContext _context;

        //como el repositorio padre tiene el dbContext inyectado, se lo pasamos del hijo al padre con base
        public VillaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<Villa> Actualizar(Villa entidad)
        {
           entidad.FechaActualizacion = DateTime.Now;
            _context.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}


//este metodo hereda toso los metodo del repositorio principal, pero adicional tiene su propio metodo