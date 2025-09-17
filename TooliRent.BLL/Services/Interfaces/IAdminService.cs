using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.BLL.Services.Interfaces
{
    public interface IAdminService
    {
        Task<AddToolResponseDto> AddTool(AddToolRequestDto toolDto);
    }
}
