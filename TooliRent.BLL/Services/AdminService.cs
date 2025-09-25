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
            var tools = _mapper.Map<Tool>(toolDto);           

            for (int i = 0; i < toolDto.Quantity; i++)
            {
                tools.ToolItems.Add(new ToolItem { Status = toolDto.Status});               
            }
            await _toolRepository.AddAsync(tools);
            await _toolRepository.SaveChangesAsync();

            return _mapper.Map<AddToolResponseDto>(tools);

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

        public async Task<bool> DeleteTool(string toolName)
        {
            var tool = await _toolRepository.GetByNameAsync(toolName);

            if (tool == null)
            {
                throw new KeyNotFoundException($"Tool with name '{toolName}' not found.");
            }

            var activeBookings = await _bookingRepository.FindAsync(b => b.ToolItems.Any(ti => ti.ToolId == tool.Id) && (b.Status == BookingStatus.Active || b.Status == BookingStatus.Pending));
            if (activeBookings.Any())
            {
                throw new InvalidOperationException("Cannot delete tool with active or pending bookings.");
            }

            await _toolRepository.DeleteAsync(tool);
            await _toolRepository.SaveChangesAsync();

            return true;

        }

        public async Task<bool> AddCategory(string categoryName)
        {
            var category = new Category
            {
                Name = categoryName
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryResponseDto?>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            
            if (categories == null || !categories.Any())
            {
                throw new KeyNotFoundException("No categories found.");
            }

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }

        public async Task<bool> UpdateCategory(string categoryName, string newCategoryName)
        {
            var category = await _categoryRepository.GetByNameAsync(categoryName);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with name '{categoryName}' not found.");
            }

            category.Name = newCategoryName;

            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCategory(string categoryName)
        {
            var category = await _categoryRepository.GetByNameAsync(categoryName);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with name '{categoryName}' not found.");
            }
            var toolsInCategory = await _toolRepository.FindAsync(t => t.CategoryId == category.Id);
            if (toolsInCategory.Any())
            {
                throw new InvalidOperationException("Cannot delete category with associated tools.");
            }

            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return true;
        }

        public async Task<LockoutUserResponse> InactivateUser(string userEmail, bool inactivate)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with email '{userEmail}' not found.");
            }

            if(inactivate == true)
            {
                if (user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.Now)
                {
                    throw new InvalidOperationException("User is already inactive.");
                }

                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.Now.AddYears(1);

                await _userManager.UpdateSecurityStampAsync(user);
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to inactivate user.");
                }
            }

            if(inactivate == false)
            {
                if(user.LockoutEnd == null || user.LockoutEnd <= DateTimeOffset.Now)
                {
                    throw new InvalidOperationException("User is already active.");
                }
                user.LockoutEnabled = false;
                user.LockoutEnd = null;
                
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to activate user.");
                }

                var map = new LockoutUserResponse
                {
                    Message = "User activated successfully",
                    Email = user.Email!,
                };

                return _mapper.Map<LockoutUserResponse>(map);
            }

            return _mapper.Map<LockoutUserResponse>(user);
        }

        public async Task<IEnumerable<ToolUsageDto>> ToolUsage()
        {                 
             var tools = await _bookingRepository.GetToolUsage();

             if (tools == null || !tools.Any())
             {
                throw new KeyNotFoundException("No popular tools found.");
             }

             return _mapper.Map<IEnumerable<ToolUsageDto>>(tools);
                          
        }

        public async Task<BorrowedToolResponseDto> GetBoorowedTools()
        {
            var bookings = await _toolRepository.GetBorrowedTools();
            if (bookings <= 0)
            {
                throw new KeyNotFoundException("No borrowed tools found.");
            }
            return _mapper.Map<BorrowedToolResponseDto>(bookings);
        }
    }
}
