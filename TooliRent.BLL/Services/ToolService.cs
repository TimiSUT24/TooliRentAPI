using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.BLL.Services.Interfaces;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.DTO;

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
            var tool = await _toolRepository.GetByIdAsync(toolName);

            if (tool == null)
            {
                throw new KeyNotFoundException($"Tool with name '{toolName}' not found.");
            }

            return _mapper.Map<ToolResponseDto?>(tool);
        }
    }
}
