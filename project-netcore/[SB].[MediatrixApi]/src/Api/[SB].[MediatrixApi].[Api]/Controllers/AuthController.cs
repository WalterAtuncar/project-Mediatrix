using Microsoft.AspNetCore.Mvc;
using _SB_._MediatrixApi_._Dominio_.Models;
using _SB_._MediatrixApi_._Servicios_.Auth;
using _SB_._MediatrixApi_._Api_.Common;

namespace _SB_._MediatrixApi_._Api_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IJwtService jwtService, ILogger<AuthController> logger)
        {
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginCredentials credentials)
        {
            try
            {
                // Aquí iría la validación real contra la base de datos
                var token = _jwtService.GenerateToken(credentials.Username);
                
                return ApiOk(new { token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el proceso de login");
                return StatusCode(500, "Error interno del servidor al procesar la solicitud");
            }
        }
    }
} 