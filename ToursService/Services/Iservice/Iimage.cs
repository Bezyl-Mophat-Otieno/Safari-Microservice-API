using ToursService.Models;

namespace ToursService.Services.Iservice
{
    public interface Iimage
    {
        Task<string> AddImage(Guid tourId, TourImage image);
    }
}
