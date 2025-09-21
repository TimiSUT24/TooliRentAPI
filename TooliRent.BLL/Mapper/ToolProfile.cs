using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRentClassLibrary.Models.DTO;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.BLL.Mapper
{
    public class ToolProfile : Profile
    {
        public ToolProfile()
        {
            CreateMap<Tool, ToolResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name)) // Place category name in the DTO
                .ForMember(dest => dest.AvailableUnits, opt => opt.MapFrom(src => src.AvailableUnits))
                .ForMember(dest => dest.BorrowedUnits, opt => opt.MapFrom(src => src.ToolItems.Count(s => s.Status == ToolStatus.Borrowed)))
                .ForMember(dest => dest.MaintenanceUnits, opt => opt.MapFrom(src => src.ToolItems.Count(s => s.Status == ToolStatus.Maintenance)))
                .ForMember(dest => dest.RetiredUnits, opt => opt.MapFrom(src => src.ToolItems.Count(s => s.Status == ToolStatus.Retired)));

            CreateMap<AddToolRequestDto, Tool>();
            CreateMap<Tool, AddToolResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Tool, AdminToolResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AvailableUnits, opt => opt.MapFrom(src => src.AvailableUnits))
                .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<(Tool tool, int count), ToolUsageDto>()
                .ForMember(dest => dest.ToolId, opt => opt.MapFrom(src => src.tool.Id))
                .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.tool.Name))
                .ForMember(dest => dest.TimesBooked, opt => opt.MapFrom(src => src.count));

            CreateMap<int, BorrowedToolResponseDto>()
                .ForMember(dest => dest.BorrowedToolCount, opt => opt.MapFrom(src => src));

        }
    }
}
