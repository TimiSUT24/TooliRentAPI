using AutoMapper;
using TooliRentClassLibrary.Models.DTO;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.BLL.Mapper
{
    public class MappingProfile : Profile
    {      
        public MappingProfile()
        {          
             CreateMap<Booking, BookingResponseDto>()
                 .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.ToolItems))
                 .ForMember(dest => dest.ToolDescription, opt => opt.MapFrom(src => src.ToolItems));

             CreateMap<BookingRequestDto, Booking>();           
        }
    }
}
