using MediatR;
using Microsoft.EntityFrameworkCore;
using _SB_._MediatrixApi_._Dominio_.Interfaces;
using _SB_._MediatrixApi_._Aplicacion_.DTOs;
using Microsoft.Extensions.Logging;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.EntidadGubernamental.Queries
{
    public class ObtenerMenuJerarquicoQuery : IRequest<MenuJerarquicoDto>
    {
        public class ObtenerMenuJerarquicoQueryHandler 
            : IRequestHandler<ObtenerMenuJerarquicoQuery, MenuJerarquicoDto>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> _repoCategorias;
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> _repoEntidades;
            private readonly ILogger<ObtenerMenuJerarquicoQueryHandler> _logger;

            public ObtenerMenuJerarquicoQueryHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> repoCategorias,
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> repoEntidades,
                ILogger<ObtenerMenuJerarquicoQueryHandler> logger)
            {
                _repoCategorias = repoCategorias;
                _repoEntidades = repoEntidades;
                _logger = logger;
            }

            public async Task<MenuJerarquicoDto> Handle(
                ObtenerMenuJerarquicoQuery request, 
                CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Obteniendo menú jerárquico");

                    // 1. Primero obtenemos las categorías activas
                    var categorias = await _repoCategorias.ObtenerQueryable()
                        .Where(c => !c.EstaEliminado)
                        .Select(c => new
                        {
                            c.CategoriaId,
                            c.Nombre,
                            c.Descripcion
                        })
                        .ToListAsync(cancellationToken);

                    // 2. Luego obtenemos las entidades activas con sus categorías
                    var entidades = await _repoEntidades.ObtenerQueryable()
                        .Where(e => !e.EstaEliminado)
                        .Select(e => new
                        {
                            e.EntidadId,
                            e.CategoriaId,
                            e.Nombre,
                            e.Siglas,
                            e.Direccion,
                            e.NombreEncargado,
                            e.FechaCreacion
                        })
                        .ToListAsync(cancellationToken);

                    // 3. Construimos el menú jerárquico en memoria
                    var categoriasMenu = categorias.Select(c => new CategoriaMenuDto
                    {
                        CategoriaId = c.CategoriaId,
                        Nombre = c.Nombre,
                        Descripcion = c.Descripcion,
                        TotalEntidades = entidades.Count(e => e.CategoriaId == c.CategoriaId),
                        Entidades = entidades
                            .Where(e => e.CategoriaId == c.CategoriaId)
                            .Select(e => new EntidadMenuDto
                            {
                                EntidadId = e.EntidadId,
                                Nombre = e.Nombre,
                                Siglas = e.Siglas ?? string.Empty,
                                Direccion = e.Direccion,
                                NombreEncargado = e.NombreEncargado,
                                FechaCreacion = e.FechaCreacion,
                                Categoria = new CategoriaEntidadDto
                                {
                                    CategoriaId = c.CategoriaId,
                                    Nombre = c.Nombre,
                                    Descripcion = c.Descripcion
                                }
                            })
                            .ToList()
                    })
                    .OrderBy(c => c.Nombre)
                    .ToList();

                    var menuJerarquico = new MenuJerarquicoDto
                    {
                        Categorias = categoriasMenu
                    };

                    _logger.LogInformation("Menú jerárquico obtenido exitosamente con {TotalCategorias} categorías", 
                        categoriasMenu.Count);
                    
                    return menuJerarquico;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener el menú jerárquico");
                    throw;
                }
            }
        }
    }
} 