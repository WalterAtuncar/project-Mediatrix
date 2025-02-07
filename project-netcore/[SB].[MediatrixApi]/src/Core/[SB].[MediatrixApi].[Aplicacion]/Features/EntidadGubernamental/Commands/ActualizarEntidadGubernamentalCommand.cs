using MediatR;
using _SB_._MediatrixApi_._Dominio_.Interfaces;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.EntidadGubernamental.Commands
{
    public class ActualizarEntidadGubernamentalCommand : IRequest<bool>
    {
        public int EntidadId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public string Siglas { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string? NombreEncargado { get; set; }

        public class ActualizarEntidadGubernamentalCommandHandler 
            : IRequestHandler<ActualizarEntidadGubernamentalCommand, bool>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> _repositorio;

            public ActualizarEntidadGubernamentalCommandHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> repositorio)
            {
                _repositorio = repositorio;
            }

            public async Task<bool> Handle(ActualizarEntidadGubernamentalCommand request, 
                CancellationToken cancellationToken)
            {
                var entidad = await _repositorio.ObtenerPorIdAsync(request.EntidadId);
                if (entidad == null || entidad.EstaEliminado)
                    throw new Exception($"No se encontró la entidad con ID: {request.EntidadId}");

                entidad.Nombre = request.Nombre;
                entidad.CategoriaId = request.CategoriaId;
                entidad.Siglas = request.Siglas;
                entidad.Direccion = request.Direccion;
                entidad.NombreEncargado = request.NombreEncargado;

                await _repositorio.ActualizarAsync(entidad);
                return true;
            }
        }
    }
} 