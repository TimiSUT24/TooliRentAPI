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
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<ApplicationUser, LoginDtoRespond>().ForMember(dest => dest.Token, opt => opt.Ignore())
                                                         .ForMember(dest => dest.RefreshToken, opt => opt.Ignore());

            CreateMap<ApplicationUser, LockoutUserResponse>();

            CreateMap<ApplicationUser, TokenRefreshResponseDto>()
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(s => s.RefreshToken))
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(opt => opt.RefreshTokenExpiryTime));

        }
       
    }
}
