using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.BLL.Services.Interfaces
{
    public interface IAdminService
    {
        Task<AddToolResponseDto> AddTool(AddToolRequestDto toolDto);
        Task<AdminToolResponseDto?> GetToolByName(string toolName);
        Task<bool> UpdateTool(string toolName, UpdateToolRequestDto toolRequest);
        Task<bool> DeleteToolItem(string toolName, int toolId);
        Task<bool> DeleteTool(string toolName); 
        Task<bool> AddCategory(string categoryName);
        Task<IEnumerable<CategoryResponseDto?>> GetCategories();
        Task<bool> UpdateCategory(string categoryName, string newCategoryName);
        Task<bool> DeleteCategory(string categoryName);
        Task<LockoutUserResponse> InactivateUser(string userEmail, bool inactivate);
    }
}
