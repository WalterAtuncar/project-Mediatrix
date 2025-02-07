using MediatR;
using Microsoft.Extensions.Logging;
using _SB_._MediatrixApi_._Dominio_.Entidades;
using _SB_._MediatrixApi_._Dominio_.Interfaces;
using _SB_._MediatrixApi_._Aplicacion_.DTOs;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.CategoriaEntidad.Queries
{
    public class ObtenerCategoriaEntidadByIdQuery : IRequest<CategoriaEntidadDto?>
    {
        public int Id { get; set; }

        public class ObtenerCategoriaEntidadByIdQueryHandler : IRequestHandler<ObtenerCategoriaEntidadByIdQuery, CategoriaEntidadDto?>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> _repositorio;
            private readonly ILogger<ObtenerCategoriaEntidadByIdQueryHandler> _logger;

            public ObtenerCategoriaEntidadByIdQueryHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> repositorio,
                ILogger<ObtenerCategoriaEntidadByIdQueryHandler> logger)
            {
                _repositorio = repositorio;
                _logger = logger;
            }

            public async Task<CategoriaEntidadDto?> Handle(ObtenerCategoriaEntidadByIdQuery request, CancellationToken cancellationToken)
            {
                var categoria = await _repositorio.ObtenerPorIdAsync(request.Id);
                
                if (categoria == null || categoria.EstaEliminado)
                    return null;

                return new CategoriaEntidadDto
                {
                    CategoriaId = categoria.CategoriaId,
                    Nombre = categoria.Nombre,
                    Descripcion = categoria.Descripcion
                };
            }
        }
    }
} 