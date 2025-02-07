using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using _SB_._MediatrixApi_._Api_.Common;
using _SB_._MediatrixApi_._Aplicacion_.Features.CategoriaEntidad.Commands;
using _SB_._MediatrixApi_._Aplicacion_.Features.CategoriaEntidad.Queries;
using System.Security.Claims;

namespace _SB_._MediatrixApi_._Api_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriaEntidadController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriaEntidadController> _logger;

        public CategoriaEntidadController(IMediator mediator, ILogger<CategoriaEntidadController> logger)
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
                _logger.LogInformation("Usuario {User} solicitando todas las categorías", user);
                
                var query = new ObtenerCategoriasEntidadesQuery();
                var resultado = await _mediator.Send(query);
                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las categorías");
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} solicitando categoría {Id}", user, id);
                
                var query = new ObtenerCategoriaEntidadByIdQuery { Id = id };
                var resultado = await _mediator.Send(query);

                if (resultado == null)
                    return ApiNotFound($"No se encontró la categoría con ID: {id}");

                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la categoría con ID: {Id}", id);
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearCategoriaEntidadCommand command)
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} creando nueva categoría", user);
                
                var resultado = await _mediator.Send(command);
                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la categoría");
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarCategoriaEntidadCommand command)
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} actualizando categoría {Id}", user, id);
                
                command.CategoriaId = id;
                var resultado = await _mediator.Send(command);
                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la categoría");
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} eliminando categoría {Id}", user, id);
                
                var command = new EliminarCategoriaEntidadCommand { Id = id };
                var resultado = await _mediator.Send(command);
                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la categoría");
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }

        [HttpGet("estadisticas")]
        public async Task<IActionResult> ObtenerEstadisticas()
        {
            try
            {
                var user = GetUser();
                _logger.LogInformation("Usuario {User} solicitando estadísticas del dashboard", user);
                
                var query = new ObtenerEstadisticasQuery();
                var resultado = await _mediator.Send(query);
                return ApiOk(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las estadísticas del dashboard");
                return ApiServerError("Error interno del servidor al procesar la solicitud");
            }
        }
    }
} 