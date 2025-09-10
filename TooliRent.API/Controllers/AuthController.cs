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
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

           var response = await _authService.RegisterUserAsync(registerDtoRequest);                  

            return Ok($"Welcome, {response.UserName}");
        }
    }
}
