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

            CreateMap<Booking, BookingDetailedResponseDto>()
                                 .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.ToolItems.FirstOrDefault()!.Tool.Name))
                                 .ForMember(dest => dest.ToolDescription, opt => opt.MapFrom(src => src.ToolItems.FirstOrDefault()!.Tool.Description))
                                 .ForMember(dest => dest.ToolId, opt => opt.MapFrom(src => src.ToolItems.FirstOrDefault()!.Tool.Id))
                                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<Booking, ReturnToolResponseDto>().ForMember(dest => dest.IsLate, opt => opt.MapFrom(src => src.IsLate));

            

        }
    }
}
