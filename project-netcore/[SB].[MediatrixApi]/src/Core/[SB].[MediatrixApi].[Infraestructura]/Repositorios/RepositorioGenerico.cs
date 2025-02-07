using _SB_._MediatrixApi_._Dominio_.Interfaces;
using _SB_._MediatrixApi_._Infraestructura_.Persistencia;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _SB_._MediatrixApi_._Infraestructura_.Repositorios
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositorioGenerico(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> ObtenerPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ObtenerTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ObtenerAsync(Expression<Func<T, bool>> predicado)
        {
            return await _dbSet.Where(predicado).ToListAsync();
        }

        public virtual async Task<bool> ExisteAsync(Expression<Func<T, bool>> predicado)
        {
            return await _dbSet.AnyAsync(predicado);
        }

        public virtual async Task<T> CrearAsync(T entidad)
        {
            await _dbSet.AddAsync(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }

        public virtual async Task<T> ActualizarAsync(T entidad)
        {
            _context.Entry(entidad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entidad;
        }

        public virtual async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            if (entidad != null)
            {
                _dbSet.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task EliminarLogicoAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            if (entidad != null)
            {
                // Asumimos que todas las entidades tienen la propiedad EstaEliminado
                var property = typeof(T).GetProperty("EstaEliminado");
                if (property != null)
                {
                    property.SetValue(entidad, true);
                    await ActualizarAsync(entidad);
                }
            }
        }

        public virtual async Task<(IEnumerable<T> Items, int Total)> ObtenerPaginadoAsync(int pagina, int registrosPorPagina)
        {
            var total = await _dbSet.CountAsync();
            var items = await _dbSet
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToListAsync();

            return (items, total);
        }

        public virtual async Task<T?> ObtenerPorIdConIncludesAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = includes.Aggregate(_dbSet.AsQueryable(),
                (current, include) => current.Include(include));

            // Buscamos la propiedad que termina en "Id"
            var parameter = Expression.Parameter(typeof(T), "x");
            var idProperty = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.EndsWith("Id"));

            if (idProperty == null)
                throw new InvalidOperationException($"No se encontró una propiedad Id en la entidad {typeof(T).Name}");

            var property = Expression.Property(parameter, idProperty);
            var value = Expression.Constant(id);
            var equal = Expression.Equal(property, value);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

            return await query.FirstOrDefaultAsync(lambda);
        }

        public virtual async Task<IEnumerable<T>> ObtenerTodosConIncludesAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = includes.Aggregate(_dbSet.AsQueryable(),
                (current, include) => current.Include(include));

            return await query.ToListAsync();
        }

        public IQueryable<T> ObtenerQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
} 