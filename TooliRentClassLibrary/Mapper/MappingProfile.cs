using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRentClassLibrary.DTO;
using TooliRentClassLibrary.Models;

namespace TooliRentClassLibrary.Mapper
{
    public class MappingProfile : Profile
    {      
        public MappingProfile()
        {
             CreateMap<Tool, ToolResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
             CreateMap<Booking, BookingResponseDto>()
                 .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.Tool.Name))
                 .ForMember(dest => dest.ToolDescription, opt => opt.MapFrom(src => src.Tool.Description));
             CreateMap<BookingRequestDto, Booking>();                   
        }
    }
}
