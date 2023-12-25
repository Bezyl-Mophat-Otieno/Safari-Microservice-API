using AutoMapper;
using BookingService.Data.Dto;
using BookingService.Models;


namespace HotelService.Profiles
{
    public class BookingProfiles:Profile
    {
        public BookingProfiles()
        {
            CreateMap<AddBookingDTO,Booking>().ReverseMap();
            
        }
    }
}
