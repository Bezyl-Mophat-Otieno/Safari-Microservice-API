using BookingService.Data;
using BookingService.Models;
using BookingService.Services.Iservice;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Services
{
    public class BookingsService : IBooking
    {

        private readonly SafariDBContext _dbContext;

        public BookingsService(SafariDBContext dBContext)
        {
            _dbContext = dBContext;
            
        }
        public async Task<string> AddBooking(Booking newbooking)
        {
            try { 

                await _dbContext.Bookings.AddAsync(newbooking);
                await _dbContext.SaveChangesAsync();

                return "Booking added successfully";
            
            }catch (Exception ex)
            {
                return "Booking Failed";

            }
        }

        public async Task<List<Booking>> GetUserBookings(Guid userid)
        {
            try
            {
                var bookings = await _dbContext.Bookings.Where(x=>x.UserId == userid).ToListAsync<Booking>();
                return bookings;

            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task<Booking> GetBookingById(Guid Id)
        {
            try
            {
                var booking = await _dbContext.Bookings.Where(x => x.Id == Id).FirstOrDefaultAsync();
                return booking;

            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task<string> UpdateBooking()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return "Updated";
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }


       
    }
}
