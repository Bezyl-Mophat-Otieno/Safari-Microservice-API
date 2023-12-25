using BookingService.Models.Dto;

namespace BookingService.Services.Iservice
{
    public interface ITour
    {
        Task<TourDTO>GetTourByID(Guid id);
    }
}
