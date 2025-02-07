using MediatR;
using _SB_._MediatrixApi_._Dominio_.Interfaces;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.CategoriaEntidad.Commands
{
    public class ActualizarCategoriaEntidadCommand : IRequest<bool>
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public class ActualizarCategoriaEntidadCommandHandler : IRequestHandler<ActualizarCategoriaEntidadCommand, bool>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> _repositorio;

            public ActualizarCategoriaEntidadCommandHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> repositorio)
            {
                _repositorio = repositorio;
            }

            public async Task<bool> Handle(ActualizarCategoriaEntidadCommand request, CancellationToken cancellationToken)
            {
                var categoria = await _repositorio.ObtenerPorIdAsync(request.CategoriaId);
                if (categoria == null || categoria.EstaEliminado)
                    throw new Exception($"No se encontró la categoría con ID: {request.CategoriaId}");

                categoria.Nombre = request.Nombre;
                categoria.Descripcion = request.Descripcion;

                await _repositorio.ActualizarAsync(categoria);
                return true;
            }
        }
    }
} 