using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterDtoRequest> RegisterUserAsync(RegisterDtoRequest registerDtoRequest);
    }
}
