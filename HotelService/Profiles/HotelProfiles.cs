using AutoMapper;
using HotelService.Models;
using HotelService.Models.Dto;

namespace HotelService.Profiles
{
    public class HotelProfiles:Profile
    {
        public HotelProfiles()
        {
            CreateMap<AddHotelDTO , Hotel>().ReverseMap();
            
        }
    }
}
