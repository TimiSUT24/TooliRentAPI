using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TooliRent.BLL.Services.Interfaces;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.DTO;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IToolRepository _toolRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AdminService(IToolRepository toolRepository, ICategoryRepository categoryRepository, IBookingRepository bookingRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _toolRepository = toolRepository;
            _categoryRepository = categoryRepository;
            _bookingRepository = bookingRepository;
            _userManager = userManager;
            _mapper = mapper;
        }      

        public async Task<AddToolResponseDto> AddTool(AddToolRequestDto toolDto)
        {
            var category = await _categoryRepository.GetByIdAsync(toolDto.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category '{toolDto.CategoryId}' not found.");
            }
            var tool = new Tool
            {
                Name = toolDto.Name,
                Description = toolDto.Description,
                CategoryId = category.Id,
                ToolItems = new List<ToolItem>()
            };

            for (int i = 0; i < toolDto.Quantity; i++)
            {
                tool.ToolItems.Add(new ToolItem { Status = toolDto.Status, ToolId = tool.Id });               
            }
            await _toolRepository.AddAsync(tool);
            await _toolRepository.SaveChangesAsync();

            return _mapper.Map<AddToolResponseDto>(tool);

        }

        public async Task<AdminToolResponseDto?> GetToolByName(string toolName)
        {
            var tool = await _toolRepository.GetByNameAsync(toolName);
            if (tool == null)
            {
                throw new KeyNotFoundException($"Tool with name '{toolName}' not found.");
            }

            return _mapper.Map<AdminToolResponseDto>(tool);
        }

        public async Task<bool> UpdateTool(string toolName, UpdateToolRequestDto toolRequest)
        {
            var tool = await _toolRepository.GetByNameAsync(toolName);
            if (tool == null)
            {
                throw new KeyNotFoundException($"Tool with name '{toolName}' not found.");
            }

            if(!string.IsNullOrEmpty(toolRequest.Name) || !string.IsNullOrEmpty(toolRequest.Name))
            {
                tool.Name = toolRequest.Name;
            }
                      
            if(!string.IsNullOrEmpty(toolRequest.Description))
            {
                tool.Description = toolRequest.Description;
            }
            if (toolRequest.CategoryId > 0)
            {
                tool.CategoryId = toolRequest.CategoryId;
            }
           
            if(toolRequest.Quantity > 0 && toolRequest.Status.HasValue)
            {
                for (int i = 0; i < toolRequest.Quantity; i++)
                {
                    tool.ToolItems.Add(new ToolItem { Status = (ToolStatus)toolRequest.Status, ToolId = tool.Id });
                }
            }          
           
            await _toolRepository.UpdateAsync(tool);
            await _toolRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteToolItem(string toolName, int toolId)
        {
            var tool = await _toolRepository.GetByNameAsync(toolName);
            if (tool == null)
            {
                throw new KeyNotFoundException($"Tool with name '{toolName}' not found.");
            }
            var toolItem = tool.ToolItems.FirstOrDefault(ti => ti.Id == toolId);

            if(toolItem == null)
            {
                throw new KeyNotFoundException($"ToolItem with Id '{toolId}' not found.");
            }

            if(toolItem.Status == ToolStatus.Borrowed)
            {
                throw new InvalidOperationException("Cannot delete a borrowed tool item.");
            }    

            await _toolRepository.DeleteToolItem(toolId);           

            return true;
        }
    }
}
