using AutoMapper;
using ToursService.Data.Dto;
using ToursService.Models;

namespace ToursService.Profiles
{
    public class ToursProfiles:Profile
    {
        public ToursProfiles()
        {
            CreateMap<AddTourDTO , Tour>().ReverseMap();
            CreateMap<AddTourImageDTO , TourImage>().ReverseMap();

        }
    }
}
