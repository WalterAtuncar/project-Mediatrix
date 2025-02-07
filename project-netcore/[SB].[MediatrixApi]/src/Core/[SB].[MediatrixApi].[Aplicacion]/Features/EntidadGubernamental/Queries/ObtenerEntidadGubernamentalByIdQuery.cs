using MediatR;
using Microsoft.Extensions.Logging;
using _SB_._MediatrixApi_._Dominio_.Entidades;
using _SB_._MediatrixApi_._Dominio_.Interfaces;
using _SB_._MediatrixApi_._Aplicacion_.DTOs;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.EntidadGubernamental.Queries
{
    public class ObtenerEntidadGubernamentalByIdQuery : IRequest<EntidadGubernamentalDto?>
    {
        public int Id { get; set; }

        public class ObtenerEntidadGubernamentalByIdQueryHandler : IRequestHandler<ObtenerEntidadGubernamentalByIdQuery, EntidadGubernamentalDto?>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> _repositorio;
            private readonly ILogger<ObtenerEntidadGubernamentalByIdQueryHandler> _logger;

            public ObtenerEntidadGubernamentalByIdQueryHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> repositorio,
                ILogger<ObtenerEntidadGubernamentalByIdQueryHandler> logger)
            {
                _repositorio = repositorio;
                _logger = logger;
            }

            public async Task<EntidadGubernamentalDto?> Handle(ObtenerEntidadGubernamentalByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Iniciando consulta de EntidadGubernamental por ID: {Id}", request.Id);
                    
                    var entidad = await _repositorio.ObtenerPorIdConIncludesAsync(request.Id, e => e.Categoria!);
                    
                    if (entidad == null)
                    {
                        _logger.LogWarning("No se encontró la EntidadGubernamental con ID: {Id}", request.Id);
                        return null;
                    }

                    var dto = new EntidadGubernamentalDto
                    {
                        EntidadId = entidad.EntidadId,
                        Nombre = entidad.Nombre,
                        Categoria = entidad.Categoria != null ? new CategoriaEntidadDto
                        {
                            CategoriaId = entidad.Categoria.CategoriaId,
                            Nombre = entidad.Categoria.Nombre,
                            Descripcion = entidad.Categoria.Descripcion
                        } : null
                    };

                    _logger.LogInformation("EntidadGubernamental encontrada. ID: {Id}, Nombre: {Nombre}", 
                        dto.EntidadId, dto.Nombre);
                    
                    return dto;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener EntidadGubernamental por ID: {Id}. Error: {Error}", 
                        request.Id, ex.Message);
                    throw;
                }
            }
        }
    }
} 