using HotelService.Models.Dto;

namespace HotelService.Services.Iservice
{
    public interface ITour
    {
        Task<TourDTO> GetTourById(Guid Id);
    }
}
