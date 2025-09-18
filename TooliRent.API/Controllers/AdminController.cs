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
                return Ok(result);
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
                return Ok($"Tool was sucessfully updated ");
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-toolItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteToolItem(string toolName, int toolId)
        {
            try
            {
                var result = await _adminService.DeleteToolItem(toolName, toolId);
                if (!result)
                {
                    return NotFound($"Tool item with id '{toolId}' not found.");
                }
                return Ok($"Tool item was successfully deleted with toolId {toolId} ");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting tool item: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-tool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTool(string toolName)
        {
            try
            {
                var result = await _adminService.DeleteTool(toolName);
                if (!result)
                {
                    return NotFound($"Tool with name '{toolName}' not found.");
                }
                return Ok($"{toolName} tool was successfully deleted.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting tool: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategory(string categoryName)
        {
            try
            {
                 var result = await _adminService.AddCategory(categoryName);

                if (!result)
                {
                    return BadRequest("Failed to add category.");
                }
             
                 return Ok($"Category '{categoryName}' was successfully created.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating category: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _adminService.GetCategories();
                if (categories == null)
                {
                    return NotFound("No categories found.");
                }
                return Ok(categories);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving categories: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-category")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> UpdateCategory(string categoryName, string newCategoryName)
        {
            try
            {
                var result = await _adminService.UpdateCategory(categoryName, newCategoryName);
                if (!result)
                {
                    return NotFound($"Category with name '{categoryName}' not found.");
                }
                return Ok($"Category '{categoryName}' was successfully updated to '{newCategoryName}'.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating category: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-category")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> DeleteCategory(string categoryName)
        {
            try
            {
                var result = await _adminService.DeleteCategory(categoryName);
                if (!result)
                {
                    return NotFound($"Category with name '{categoryName}' not found.");
                }
                return Ok($"Category '{categoryName}' was successfully deleted.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting category: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("inactivate/reactivate-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InactivateUser(string userEmail, bool inactivate)
        {
            try
            {
                var result = await _adminService.InactivateUser(userEmail, inactivate);
                if (result == null)
                {
                    return NotFound($"User with email '{userEmail}' not found.");
                }
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error inactivating user: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("statistics-popular-tools")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ToolUsage()
        {
            try
            {
                var stats = await _adminService.ToolUsage();
                if(stats == null)
                {
                    return BadRequest("No statistics available.");
                }
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving statistics: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("statistics-borrowed-tools")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBorrowedTools()
        {
            try
            {
                var count = await _adminService.GetBoorowedTools();
               

                return Ok(count);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving borrowed tools count: {ex.Message}");
            }
        }
    }
}
