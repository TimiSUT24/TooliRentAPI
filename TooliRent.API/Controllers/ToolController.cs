using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TooliRent.BLL.Services.Interfaces;

namespace TooliRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly IToolService _toolService;
        public ToolController(IToolService toolService)
        {
            _toolService = toolService;
        }

        [HttpGet("available-tools")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAvailableTools()
        {
            try
            {
                var tools = await _toolService.AvailableTools();

                if(tools == null || !tools.Any())
                {
                    return NotFound("No available tools found.");
                }

                return Ok(tools);
            }

            catch (Exception ex)
            {
                return NotFound($"No tools were found: {ex.Message}");
            }
        }     
    }
}
