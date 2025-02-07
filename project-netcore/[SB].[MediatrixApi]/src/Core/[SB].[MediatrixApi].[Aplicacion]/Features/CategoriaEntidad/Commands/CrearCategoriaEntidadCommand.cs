using MediatR;
using Microsoft.Extensions.Logging;
using _SB_._MediatrixApi_._Dominio_.Entidades;
using _SB_._MediatrixApi_._Dominio_.Interfaces;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.CategoriaEntidad.Commands
{
    public class CrearCategoriaEntidadCommand : IRequest<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad>
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public class CrearCategoriaEntidadCommandHandler : IRequestHandler<CrearCategoriaEntidadCommand, _SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> _repositorio;
            private readonly ILogger<CrearCategoriaEntidadCommandHandler> _logger;

            public CrearCategoriaEntidadCommandHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> repositorio,
                ILogger<CrearCategoriaEntidadCommandHandler> logger)
            {
                _repositorio = repositorio;
                _logger = logger;
            }

            public async Task<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> Handle(CrearCategoriaEntidadCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Iniciando creación de CategoriaEntidad: {Nombre}", request.Nombre);

                    var entidad = new _SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad
                    {
                        Nombre = request.Nombre,
                        Descripcion = request.Descripcion,
                        EstaEliminado = false
                    };

                    var resultado = await _repositorio.CrearAsync(entidad);
                    
                    _logger.LogInformation("CategoriaEntidad creada exitosamente. ID: {Id}", resultado.CategoriaId);
                    return resultado;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear CategoriaEntidad: {Nombre}. Error: {Error}", 
                        request.Nombre, ex.Message);
                    throw;
                }
            }
        }
    }
} 