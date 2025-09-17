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
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name)); // Place category name in the DTO

            CreateMap<AddToolRequestDto, Tool>();
            CreateMap<Tool, AddToolResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Tool, AdminToolResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AvailableUnits, opt => opt.MapFrom(src => src.AvailableUnits))
                .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.Quantity));


        }
    }
}
