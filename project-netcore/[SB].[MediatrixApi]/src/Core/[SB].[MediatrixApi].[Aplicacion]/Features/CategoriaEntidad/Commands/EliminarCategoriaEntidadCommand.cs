using MediatR;
using _SB_._MediatrixApi_._Dominio_.Interfaces;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.CategoriaEntidad.Commands
{
    public class EliminarCategoriaEntidadCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public class EliminarCategoriaEntidadCommandHandler : IRequestHandler<EliminarCategoriaEntidadCommand, bool>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> _repositorio;

            public EliminarCategoriaEntidadCommandHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.CategoriaEntidad> repositorio)
            {
                _repositorio = repositorio;
            }

            public async Task<bool> Handle(EliminarCategoriaEntidadCommand request, CancellationToken cancellationToken)
            {
                await _repositorio.EliminarLogicoAsync(request.Id);
                return true;
            }
        }
    }
} 