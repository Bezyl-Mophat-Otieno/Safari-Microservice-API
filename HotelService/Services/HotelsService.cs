using HotelService.Data;
using HotelService.Models;
using HotelService.Services.Iservice;

namespace HotelService.Services
{
    public class HotelsService : IHotel
    {

        private readonly SafariDBContext _dbContext;

        public HotelsService(SafariDBContext dBContext)
        {
            _dbContext = dBContext;
            
        }
        public async Task<string> AddHotel(Hotel hotel)
        {
            try {

                _dbContext.Add(hotel);
                await _dbContext.SaveChangesAsync();

                return "Hotel Added "; 
            
            }catch(Exception ex)
            {
                return "Failed to add a Hotel";

            }
        }

        public async Task<Hotel> GetHotelById(Guid id)
        {
            try
            {
                var hotel = await _dbContext.Hotels.FindAsync(id);
                return hotel;

            }
            catch (Exception ex)
            {

                return null;

            }
        }

        public async Task<List<Hotel>> GetHotelsByTourLocation(Guid TourId)
        {
            try
            {

                var hotels = _dbContext.Hotels.Where(x=>x.TourId == TourId).ToList<Hotel>();
                    
                return hotels;
            }
            catch (Exception ex)
            {
                return new List<Hotel>();

            }
        }
    }
}
