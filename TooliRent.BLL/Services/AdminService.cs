using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TooliRent.BLL.Services.Interfaces;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IToolService _toolService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AdminService(IToolService toolService, ICategoryRepository categoryRepository, IBookingRepository bookingRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _toolService = toolService;
            _categoryRepository = categoryRepository;
            _bookingRepository = bookingRepository;
            _userManager = userManager;
            _mapper = mapper;
        }      
    }
}
