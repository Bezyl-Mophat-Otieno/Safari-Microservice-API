using HotelService.Models;

namespace HotelService.Services.Iservice
{
    public interface IHotel
    {
        Task<Hotel>GetHotelById(Guid id);

        Task<string>AddHotel(Hotel hotel);
        Task<string>UpdateHotel();
        Task<bool>DeleteHotel(Hotel hotel);

        Task<List<Hotel>> GetHotelsByTourLocation(Guid TourId);
    }
}
