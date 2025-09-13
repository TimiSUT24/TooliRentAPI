using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TooliRent.BLL.Services.Interfaces;
using TooliRentClassLibrary.Models.Models;

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

        [HttpGet("{toolName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetToolByName(string toolName)
        {
            try
            {
                var tool = await _toolService.GetToolByName(toolName);
                if (tool == null)
                {
                    return NotFound($"Tool with name '{toolName}' not found.");
                }
                return Ok(tool);
            }
            catch (Exception ex)
            {
                return NotFound($"Error retrieving tool: {ex.Message}");
            }
        }

        [HttpGet("filterTools")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFilteredTools(string? categoryName = null,ToolStatus? status = null,bool onlyAvailable = false)
        {
            try
            {
                var tools = await _toolService.GetFilteredToolsAsync(categoryName, status, onlyAvailable);
                if (tools == null || !tools.Any())
                {
                    return NotFound("No tools found matching the specified criteria.");
                }
                return Ok(tools);
            }
            catch (Exception ex)
            {
                return NotFound($"Error retrieving tools: {ex.Message}");
            }
        }
    }
}
