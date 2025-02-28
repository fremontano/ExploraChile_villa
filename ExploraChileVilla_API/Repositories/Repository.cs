using ExploraChileVilla_API.Data;
using ExploraChileVilla_API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ExploraChileVilla_API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Trabajaremos con nuestro dbContext y no en el controlador
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;





        public Repository(ApplicationDbContext context)
        {
            _context = context; // Se inyecta el contexto de BD
            this.dbSet = _context.Set<T>(); // Se asigna la tabla correspondiente a T
        }



        public async Task Crear(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Guardar();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            return await query.FirstOrDefaultAsync();
        }



        public async Task<List<T>> ObtenerTodo(Expression<Func<T, bool>> filtro = null)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            return await query.ToListAsync();
        }



        public async Task Guardar()
        {
            await _context.SaveChangesAsync();
        }


        public async Task Remover(T entidad)
        {
            dbSet.Remove(entidad);
            await Guardar();
        }


    }
}
