using BookingService.Data.Dto;
using BookingService.Models;

namespace BookingService.Services.Iservice
{
    public interface IBooking
    {
        Task<string> AddBooking(Booking newbooking);
        Task<string> UpdateBooking();
        Task<List<Booking>> GetUserBookings(Guid userid);
        Task<Booking> GetBookingById(Guid Id);

        Task<StripeRequestDTO> MakePayments (StripeRequestDTO stripeRequestDTO);

        Task<bool> ValidatePayments(Guid BookingId);


    }
}
