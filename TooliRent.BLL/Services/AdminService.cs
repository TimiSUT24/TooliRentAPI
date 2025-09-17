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
    }
}
