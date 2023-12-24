using Microsoft.EntityFrameworkCore;
using ToursService.Data;
using ToursService.Data.Dto;
using ToursService.Models;
using ToursService.Services.Iservice;

namespace ToursService.Services
{
    public class ToursafariService : ITour
    {

        private readonly SafariDBContext _dbContext;

        public ToursafariService( SafariDBContext dBContext)
        {
            _dbContext = dBContext;
            
        }
        public async Task<string> AddTour(Tour tour)
        {
            try {


                await _dbContext.Tours.AddAsync(tour);
                await _dbContext.SaveChangesAsync();

                return "Tour added";

            
            
            }catch (Exception ex) {

                return ex.Message;
            
            }
        }

        public async Task<bool> DeleteTourAsync(Tour tour)
        {
                try
                {

                    _dbContext.Remove(tour);
                    await _dbContext.SaveChangesAsync();

                    return true;

                }
                catch (Exception ex)
                {

                    return false;
                
            }
        }

        public async Task<List<ToursandImagesResponseDTO>> GetAllToursAsync()
        {
            try {

                var tours = await _dbContext.Tours.Select(t => new ToursandImagesResponseDTO()
                {
                    Id= t.Id,
                    SafariName = t.SafariName,
                    SafariDescription = t.SafariDescription,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Price = t.Price,
                    TourImages = t.SafariImages.Select(i => new AddTourImageDTO()
                    {
                        ImageUrl=i.ImageUrl

                    }).ToList(),
                }).ToListAsync();;

                return tours;

 
            
            }catch(Exception ex)
            {
                return new List<ToursandImagesResponseDTO>();
            }
        }

        public async Task<Tour> GetTourAsync(Guid Id)
        {
            try {
                var tour = await _dbContext.Tours.FindAsync(Id);
                return tour;     
            }catch (Exception ex) {

                return null;
            

            
            }


        }

        public async Task<string> UpdateTourAsync()
        {
            try
            {

                await _dbContext.SaveChangesAsync();

                return "Tour Information Updated Successfully";



            }
            catch (Exception ex)
            {
                return "Failed to Update Tour Information";

            }
        }
    }
}
