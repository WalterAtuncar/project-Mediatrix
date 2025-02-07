using System.Linq.Expressions;

namespace _SB_._MediatrixApi_._Dominio_.Interfaces
{
    public interface IRepositorioGenerico<T> where T : class
    {
        // Operaciones de Lectura
        Task<T?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<IEnumerable<T>> ObtenerAsync(Expression<Func<T, bool>> predicado);
        Task<bool> ExisteAsync(Expression<Func<T, bool>> predicado);

        // Operaciones de Escritura
        Task<T> CrearAsync(T entidad);
        Task<T> ActualizarAsync(T entidad);
        Task EliminarAsync(int id);
        Task EliminarLogicoAsync(int id);

        // Operaciones de Paginación
        Task<(IEnumerable<T> Items, int Total)> ObtenerPaginadoAsync(int pagina, int registrosPorPagina);
        
        // Operaciones con Include (para relaciones)
        Task<T?> ObtenerPorIdConIncludesAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> ObtenerTodosConIncludesAsync(params Expression<Func<T, object>>[] includes);

        IQueryable<T> ObtenerQueryable();
    }
} 