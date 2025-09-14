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
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingResponseDto>()
                                 .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.ToolItems.FirstOrDefault()!.Tool.Name))
                                 .ForMember(dest => dest.ToolDescription, opt => opt.MapFrom(src => src.ToolItems.FirstOrDefault()!.Tool.Description))
                                 .ForMember(dest => dest.ToolId, opt => opt.MapFrom(src => src.ToolItems.FirstOrDefault()!.Tool.Id));


        }
    }
}
