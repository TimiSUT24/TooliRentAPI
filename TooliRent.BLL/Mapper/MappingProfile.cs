using AutoMapper;
using TooliRentClassLibrary.Models.DTO;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.BLL.Mapper
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
