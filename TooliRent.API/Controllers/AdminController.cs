using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TooliRent.BLL.Services.Interfaces;
using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-tool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddTool([FromBody] AddToolRequestDto addToolRequest)
        {
            try
            {
                var result = await _adminService.AddTool(addToolRequest);
                if(result == null)
                {
                    return BadRequest("Failed to add tool.");
                }
                return Ok($"Added tool {result.Name} " + result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("tool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetToolByName([FromQuery] string toolName)
        {
            try
            {
                var tool = await _adminService.GetToolByName(toolName);
                if (tool == null)
                {
                    return NotFound($"Tool with name '{toolName}' not found.");
                }
                return Ok(tool);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving tool: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-tools")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTool([FromBody] UpdateToolRequestDto updateToolRequest, string toolName)
        {
            try
            {
                var updatedTool = await _adminService.UpdateTool(toolName , updateToolRequest);
                if (!updatedTool)
                {
                    return NotFound($"Tool with name '{updateToolRequest.Name}' not found.");
                }
                return Ok(updatedTool);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating tool: {ex.Message}");
            }
        }
    }
}
