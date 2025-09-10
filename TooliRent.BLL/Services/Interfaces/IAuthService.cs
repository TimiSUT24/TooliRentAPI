using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterDtoResponse?> RegisterUserAsync(RegisterDtoRequest registerDtoRequest);
    }
}
