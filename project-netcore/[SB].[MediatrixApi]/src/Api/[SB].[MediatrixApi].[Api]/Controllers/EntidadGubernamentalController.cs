using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using _SB_._MediatrixApi_._Api_.Common;
using _SB_._MediatrixApi_._Aplicacion_.Features.EntidadGubernamental.Commands;
using _SB_._MediatrixApi_._Aplicacion_.Features.EntidadGubernamental.Queries;

namespace _SB_._MediatrixApi_._Api_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EntidadGubernamentalController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EntidadGubernamentalController> _logger;

        public EntidadGubernamentalController(IMediator mediator, ILogger<EntidadGubernamentalController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} solicitando todas las entidades gubernamentales", user);
                
                var query = new ObtenerEntidadesGubernamentalesQuery();
                var resultado = await _mediator.Send(query);
                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las entidades gubernamentales");
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} solicitando entidad gubernamental {Id}", user, id);
                
                var query = new ObtenerEntidadGubernamentalByIdQuery { Id = id };
                var resultado = await _mediator.Send(query);

                if (resultado == null)
                    return ApiNotFound($"No se encontró la entidad gubernamental con ID: {id}");

                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la entidad gubernamental con ID: {Id}", id);
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] RegistrarEntidadGubernamentalCommand command)
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} creando nueva entidad gubernamental", user);
                
                var resultado = await _mediator.Send(command);
                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la entidad gubernamental");
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarEntidadGubernamentalCommand command)
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} actualizando entidad gubernamental {Id}", user, id);
                
                command.EntidadId = id;
                var resultado = await _mediator.Send(command);
                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la entidad gubernamental");
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} eliminando entidad gubernamental {Id}", user, id);
                
                var command = new EliminarEntidadGubernamentalCommand { Id = id };
                var resultado = await _mediator.Send(command);
                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la entidad gubernamental");
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }

        [HttpGet("menu-jerarquico")]
        public async Task<IActionResult> ObtenerMenuJerarquico()
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} solicitando menú jerárquico", user);
                
                var query = new ObtenerMenuJerarquicoQuery();
                var resultado = await _mediator.Send(query);
                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el menú jerárquico");
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }
    }
} 