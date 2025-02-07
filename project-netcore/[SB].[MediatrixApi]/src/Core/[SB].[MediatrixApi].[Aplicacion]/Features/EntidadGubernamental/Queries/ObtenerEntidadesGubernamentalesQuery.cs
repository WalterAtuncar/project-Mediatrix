using MediatR;
using Microsoft.Extensions.Logging;
using _SB_._MediatrixApi_._Dominio_.Interfaces;
using _SB_._MediatrixApi_._Aplicacion_.DTOs;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.EntidadGubernamental.Queries
{
    public class ObtenerEntidadesGubernamentalesQuery : IRequest<IEnumerable<EntidadGubernamentalDto>>
    {
        public class ObtenerEntidadesGubernamentalesQueryHandler 
            : IRequestHandler<ObtenerEntidadesGubernamentalesQuery, IEnumerable<EntidadGubernamentalDto>>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> _repositorio;
            private readonly ILogger<ObtenerEntidadesGubernamentalesQueryHandler> _logger;

            public ObtenerEntidadesGubernamentalesQueryHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> repositorio,
                ILogger<ObtenerEntidadesGubernamentalesQueryHandler> logger)
            {
                _repositorio = repositorio;
                _logger = logger;
            }

            public async Task<IEnumerable<EntidadGubernamentalDto>> Handle(
                ObtenerEntidadesGubernamentalesQuery request, 
                CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Obteniendo todas las entidades gubernamentales");
                    
                    var entidades = await _repositorio.ObtenerTodosConIncludesAsync(e => e.Categoria!);
                    
                    var resultado = entidades.Select(e => new EntidadGubernamentalDto
                    {
                        EntidadId = e.EntidadId,
                        Nombre = e.Nombre,
                        Siglas = e.Siglas ?? string.Empty,
                        CategoriaId = e.CategoriaId,
                        Direccion = e.Direccion,
                        NombreEncargado = e.NombreEncargado,
                        FechaCreacion = e.FechaCreacion,
                        EstaEliminado = e.EstaEliminado,
                        Categoria = e.Categoria != null ? new CategoriaEntidadDto
                        {
                            CategoriaId = e.Categoria.CategoriaId,
                            Nombre = e.Categoria.Nombre,
                            Descripcion = e.Categoria.Descripcion
                        } : null
                    });

                    _logger.LogInformation("Se obtuvieron {Count} entidades gubernamentales", resultado.Count());
                    
                    return resultado;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener las entidades gubernamentales");
                    throw;
                }
            }
        }
    }
} 