using AutoMapper;
using SafariAuthService.Data.Dto;
using SafariAuthService.Models;

namespace SafariAuthService.Profiles
{
    public class AuthProfile:Profile
    {

        public AuthProfile()
        {
            CreateMap<RegisterUserDTO, ApplicationUser>().ForMember(destination=>destination.UserName , source=>source.MapFrom(r=>r.Email));

            CreateMap<UserDTO, ApplicationUser>().ReverseMap();
        }
    }
}
