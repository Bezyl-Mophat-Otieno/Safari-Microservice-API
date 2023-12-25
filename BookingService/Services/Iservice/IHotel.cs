using BookingService.Models.Dto;

namespace BookingService.Services.Iservice
{
    public interface IHotel
    {
        Task<HotelDTO>GetHotelById(Guid id);
    }
}
