using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TooliRent.BLL.Services.Interfaces;
using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 400)]
        public async Task<IActionResult> Register(RegisterDtoRequest registerDtoRequest)
        {         

            var response = await _authService.RegisterUserAsync(registerDtoRequest);

            if (response == null)
            {
                return BadRequest("Registration failed");
            }

            return Ok($"Welcome, {response.UserName}");
        }

        [HttpPost("login")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 401)]
        public async Task<IActionResult> Login(LoginDtoRequest loginDtoRequest)
        {
            try
            {
                var response = await _authService.LoginAsync(loginDtoRequest);

                if (response == null)
                {
                    return Unauthorized("Invalid email or password");
                }

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }          
        }
    }
}
