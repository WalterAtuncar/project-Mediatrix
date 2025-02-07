using MediatR;
using Microsoft.Extensions.Logging;
using _SB_._MediatrixApi_._Dominio_.Interfaces;
using _SB_._MediatrixApi_._Aplicacion_.DTOs;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.CategoriaEntidad.Queries
{
    public class ObtenerEstadisticasQuery : IRequest<EstadisticasDto>
    {
        public class ObtenerEstadisticasQueryHandler : IRequestHandler<ObtenerEstadisticasQuery, EstadisticasDto>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> _repoCategorias;
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> _repoEntidades;
            private readonly ILogger<ObtenerEstadisticasQueryHandler> _logger;

            public ObtenerEstadisticasQueryHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> repoCategorias,
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> repoEntidades,
                ILogger<ObtenerEstadisticasQueryHandler> logger)
            {
                _repoCategorias = repoCategorias;
                _repoEntidades = repoEntidades;
                _logger = logger;
            }

            public async Task<EstadisticasDto> Handle(ObtenerEstadisticasQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Obteniendo estadísticas del dashboard");

                    var categorias = await _repoCategorias.ObtenerTodosAsync();
                    var entidades = await _repoEntidades.ObtenerTodosAsync();

                    var resultado = new EstadisticasDto
                    {
                        TotalCategorias = categorias.Count(),
                        TotalEntidades = entidades.Count()
                    };

                    _logger.LogInformation("Estadísticas obtenidas: Categorías: {TotalCategorias}, Entidades: {TotalEntidades}", 
                        resultado.TotalCategorias, resultado.TotalEntidades);

                    return resultado;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener las estadísticas del dashboard");
                    throw;
                }
            }
        }
    }
} 