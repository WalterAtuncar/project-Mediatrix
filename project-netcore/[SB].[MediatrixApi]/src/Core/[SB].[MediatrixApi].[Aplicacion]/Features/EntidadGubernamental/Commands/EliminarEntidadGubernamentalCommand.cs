using MediatR;
using _SB_._MediatrixApi_._Dominio_.Interfaces;

namespace _SB_._MediatrixApi_._Aplicacion_.Features.EntidadGubernamental.Commands
{
    public class EliminarEntidadGubernamentalCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public class EliminarEntidadGubernamentalCommandHandler 
            : IRequestHandler<EliminarEntidadGubernamentalCommand, bool>
        {
            private readonly IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> _repositorio;

            public EliminarEntidadGubernamentalCommandHandler(
                IRepositorioGenerico<_SB_._MediatrixApi_._Dominio_.Entidades.EntidadGubernamental> repositorio)
            {
                _repositorio = repositorio;
            }

            public async Task<bool> Handle(EliminarEntidadGubernamentalCommand request, 
                CancellationToken cancellationToken)
            {
                await _repositorio.EliminarLogicoAsync(request.Id);
                return true;
            }
        }
    }
} 