using MediatR;
using Microsoft.Extensions.Logging;
using _SB_._MediatrixApi_._Dominio_.Entidades;
using _SB_._MediatrixApi_._Dominio_.Interfaces;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.EntidadGubernamental.Commands
{
    public class RegistrarEntidadGubernamentalCommand : IRequest<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental>
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Siglas { get; set; }
        public int CategoriaId { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string? NombreEncargado { get; set; }

        public class RegistrarEntidadGubernamentalCommandHandler : IRequestHandler<RegistrarEntidadGubernamentalCommand, _SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> _repositorio;
            private readonly ILogger<RegistrarEntidadGubernamentalCommandHandler> _logger;

            public RegistrarEntidadGubernamentalCommandHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> repositorio,
                ILogger<RegistrarEntidadGubernamentalCommandHandler> logger)
            {
                _repositorio = repositorio;
                _logger = logger;
            }

            public async Task<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> Handle(RegistrarEntidadGubernamentalCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Iniciando registro de EntidadGubernamental: {Nombre}, CategoriaId: {CategoriaId}", 
                        request.Nombre, request.CategoriaId);

                    var entidad = new _SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental
                    {
                        Nombre = request.Nombre,
                        Siglas = request.Siglas,
                        CategoriaId = request.CategoriaId,
                        Direccion = request.Direccion,
                        NombreEncargado = request.NombreEncargado,
                        FechaCreacion = DateTime.UtcNow,
                        EstaEliminado = false
                    };

                    var resultado = await _repositorio.CrearAsync(entidad);
                    
                    _logger.LogInformation("EntidadGubernamental registrada exitosamente. ID: {Id}", 
                        resultado.EntidadId);
                    
                    return resultado;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al registrar EntidadGubernamental: {Nombre}. Error: {Error}", 
                        request.Nombre, ex.Message);
                    throw;
                }
            }
        }
    }
} 