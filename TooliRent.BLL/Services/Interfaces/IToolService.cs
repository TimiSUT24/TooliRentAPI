using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRentClassLibrary.Models.DTO;
using TooliRentClassLibrary.Models.DTO.Interfaces;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.BLL.Services.Interfaces
{
    public interface IToolService
    {
        Task<IEnumerable<ToolResponseDto?>> AvailableTools();
        Task<ToolResponseDto?> GetToolByName(string name);
        Task<IEnumerable<IToolResponseDto>> GetFilteredToolsAsync(string? categoryName = null, ToolStatus? status = null, bool onlyavailable = false);
    }
}
