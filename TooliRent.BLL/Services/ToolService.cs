using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.BLL.Services.Interfaces;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.DTO;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.BLL.Services
{
    public class ToolService : IToolService
    {
        private readonly IToolRepository _toolRepository;
        private readonly IMapper _mapper;
        public ToolService(IToolRepository toolRepository, IMapper mapper)
        {
            _toolRepository = toolRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToolResponseDto?>> AvailableTools()
        {
            var tools = await _toolRepository.GetAllAsync();

            if(tools == null || !tools.Any())
            {
                throw new KeyNotFoundException("No available tools found.");
            }

            return _mapper.Map<IEnumerable<ToolResponseDto?>>(tools);
        }

        public async Task<ToolResponseDto?> GetToolByName(string toolName)
        {
            var tool = await _toolRepository.GetByNameAsync(toolName);

            if (tool == null)
            {
                throw new KeyNotFoundException($"Tool with name '{toolName}' not found.");
            }

            return _mapper.Map<ToolResponseDto?>(tool);
        }

        public async Task<IEnumerable<ToolResponseDto?>> GetFilteredToolsAsync(string? categoryName = null, ToolStatus? status = null, bool onlyavailable = false)
        {
            var tool = await _toolRepository.GetFilteredToolsAsync(categoryName, status, onlyavailable);
            
            if (tool == null)
            {
                throw new KeyNotFoundException("tool were not found");
            }

            return _mapper.Map<IEnumerable<ToolResponseDto>>(tool);
        }
    }
}
