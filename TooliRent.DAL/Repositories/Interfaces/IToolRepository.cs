using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Repositories.Interfaces
{
    public interface IToolRepository : IGenericRepository<Tool>
    {
        Task<IEnumerable<Tool>> GetFilteredToolsAsync(string? categoryName = null, ToolStatus? status = null, bool? onlyAvailable = null);
    }
}
