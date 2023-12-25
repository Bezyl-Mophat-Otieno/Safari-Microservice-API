using BookingService.Data;
using BookingService.Data.Dto;
using BookingService.Models;
using BookingService.Services.Iservice;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace BookingService.Services
{
    public class BookingsService : IBooking
    {

        private readonly SafariDBContext _dbContext;

        private readonly ITour _tourservice;


        public BookingsService(SafariDBContext dBContext , ITour tourservice)
        {
            _dbContext = dBContext;

            _tourservice = tourservice;

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

        public async Task<StripeRequestDTO> MakePayments(StripeRequestDTO stripeRequestDTO)
        {
            try {

                var booking = await _dbContext.Bookings.Where(x => x.Id == stripeRequestDTO.BookingId).FirstOrDefaultAsync();

                var tour = await _tourservice.GetTourByID(booking.TourId);

                var options = new SessionCreateOptions()
                {
                    SuccessUrl = stripeRequestDTO.ApprovedUrl,
                    CancelUrl = stripeRequestDTO.CancelUrl,
                    Mode = "payment",
                    LineItems = new List<SessionLineItemOptions>()
                };


                var Item = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = (long)booking.BookingTotal * 100,
                        Currency = "Kes",
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = tour.SafariName,
                            Description = tour.SafariDescription,
                            Images = new List<string> { "https://www.bing.com/ck/a?!&&p=aa10fbc84d1d61efJmltdHM9MTcwMzQ2MjQwMCZpZ3VpZD0zYTc2NzRkMS0xZDEwLTY0OTEtMjU0NS02NzBlMWM4NjY1NjImaW5zaWQ9NTY2OA&ptn=3&ver=2&hsh=3&fclid=3a7674d1-1d10-6491-2545-670e1c866562&u=a1L2ltYWdlcy9zZWFyY2g_cT1tYWFzYWkgbWFyYSBpbWFnZXMmRk9STT1JUUZSQkEmaWQ9NDFFRjdGQzMwQUI5Q0JBM0Y5QzU3NDJFN0YzNkNBNDY5NjBGODFFOQ&ntb=1" }
                        },
        
                    },
                    Quantity = 1
                    
                };
                options.LineItems.Add(Item);

                var discountObj = new List<SessionDiscountOptions>()
                {
                    new SessionDiscountOptions()
                    {
                        Coupon = booking.CouponCode
                    }
                };
                // All this will be done when booking discount is greater than 0

                if(booking.Discount > 0)
                {
                    options.Discounts = discountObj;

                }

                var service = new SessionService();

                Session session = service.Create(options);

                stripeRequestDTO.StripeSessionUrl = session.Url;
                stripeRequestDTO.StripeSessionId = session.Id;

                // Update the Database booking with the status and session Id .

                booking.StripeSessionId = session.Id;
                booking.Status = session.Status;

                await _dbContext.SaveChangesAsync();

                return stripeRequestDTO;


            }
            catch (Exception ex)
            {
                return new StripeRequestDTO() { ApprovedUrl = ex.Message };

            }
        }

        public async Task<bool> ValidatePayments(Guid BookingId)
        {
            try {
                var booking = await _dbContext.Bookings.Where(x => x.Id == BookingId).FirstOrDefaultAsync();

                var service = new SessionService();

                Session session = service.Get(booking.StripeSessionId);

                PaymentIntentService paymentIntentService = new PaymentIntentService();

                PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId); 


                if(paymentIntent.Status == "succeeded")
                {
                    booking.Status = "Paid";
                    booking.PaymentIntent = paymentIntent.Id;
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            
            
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
