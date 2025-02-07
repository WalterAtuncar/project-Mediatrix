using Microsoft.AspNetCore.Mvc;
using _SB_._MediatrixApi_._Dominio_.Common;
using System.Security.Claims;

namespace _SB_._MediatrixApi_._Api_.Common
{
    public class ApiControllerBase : ControllerBase
    {
        protected string GetUser()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        protected IActionResult ApiOk<T>(T data)
        {
            var response = ApiResponse<T>.Success(data);
            return Ok(response);
        }

        protected IActionResult ApiBadRequest(string message)
        {
            var response = ApiResponse<object>.Failed(message, 400);
            return BadRequest(response);
        }

        protected IActionResult ApiNotFound(string message)
        {
            var response = ApiResponse<object>.Failed(message, 404);
            return NotFound(response);
        }

        protected IActionResult ApiServerError(string message)
        {
            var response = ApiResponse<object>.Failed(message, 500);
            return StatusCode(500, response);
        }
    }
} 