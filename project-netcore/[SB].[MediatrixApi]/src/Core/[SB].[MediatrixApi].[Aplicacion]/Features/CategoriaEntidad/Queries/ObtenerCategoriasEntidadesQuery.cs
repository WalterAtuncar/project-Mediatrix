using MediatR;
using Microsoft.Extensions.Logging;
using _SB_._MediatrixApi_._Dominio_.Entidades;
using _SB_._MediatrixApi_._Dominio_.Interfaces;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.CategoriaEntidad.Queries
{
    public class ObtenerCategoriasEntidadesQuery : IRequest<IEnumerable<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad>>
    {
        public class ObtenerCategoriasEntidadesQueryHandler : IRequestHandler<ObtenerCategoriasEntidadesQuery, IEnumerable<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad>>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> _repositorio;
            private readonly ILogger<ObtenerCategoriasEntidadesQueryHandler> _logger;

            public ObtenerCategoriasEntidadesQueryHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> repositorio,
                ILogger<ObtenerCategoriasEntidadesQueryHandler> logger)
            {
                _repositorio = repositorio;
                _logger = logger;
            }

            public async Task<IEnumerable<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad>> Handle(ObtenerCategoriasEntidadesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Iniciando consulta de todas las CategoriaEntidad");
                    
                    var resultado = await _repositorio.ObtenerTodosAsync();
                    
                    _logger.LogInformation("Consulta de CategoriaEntidad completada. Total registros: {Count}", 
                        resultado.Count());
                    
                    return resultado;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener las CategoriaEntidad. Error: {Error}", ex.Message);
                    throw;
                }
            }
        }
    }
} 