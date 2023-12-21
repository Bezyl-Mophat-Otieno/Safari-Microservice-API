using ToursService.Data;
using ToursService.Models;
using ToursService.Services.Iservice;

namespace ToursService.Services
{
    public class ImagetourService : Iimage
    {

        private readonly SafariDBContext _dBContext;

        public ImagetourService(SafariDBContext dbContext)
        {

            _dBContext = dbContext;
            
        }
        public async Task<string> AddImage(Guid tourId, TourImage image)
        {
            try {

                var tour =await  _dBContext.Tours.FindAsync(tourId);

                if (tour == null)
                {
                    return "Tour Doesnot exist";

                }

                 tour.SafariImages.Add(image);
                await _dBContext.SaveChangesAsync();
                return "Image added successfully";

                await _dBContext.SaveChangesAsync();
            }catch (Exception ex) {

                return ex.Message;
            
            }
        }
    }
}
