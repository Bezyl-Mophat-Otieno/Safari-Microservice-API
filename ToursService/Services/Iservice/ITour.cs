using ToursService.Data.Dto;
using ToursService.Models;

namespace ToursService.Services.Iservice
{
    public interface ITour
    {
        Task<List<ToursandImagesResponseDTO>> GetAllToursAsync();

        Task<Tour> GetTourAsync(Guid Id);

        Task<string> AddTour(Tour tour);

        Task<string> UpdateTourAsync();
        Task<bool> DeleteTourAsync(Tour tour);

    }
}
