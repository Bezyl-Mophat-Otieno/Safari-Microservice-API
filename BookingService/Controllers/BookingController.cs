using AutoMapper;
using BookingService.Data.Dto;
using BookingService.Models;
using BookingService.Services.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IBooking _bookingservice;
        private readonly ITour _tourservice;
        private readonly IHotel _hotelservice;
        private readonly ICoupon _couponservice;
        private readonly ResponseDTO _response;

        public BookingController(IHotel hotelservice , ITour tourservice , IBooking bookingservice , IMapper mapper , ICoupon couponservice)
        {
            _hotelservice = hotelservice;
            _tourservice = tourservice;
            _mapper = mapper;
            _bookingservice = bookingservice;
            _couponservice = couponservice;
            _response = new ResponseDTO();
        }
        [HttpGet("{userId}")]

       // public async Task<ActionResult<ResponseDTO>> GetAllBookings(Guid userId) { }
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddBooking(AddBookingDTO newbooking) {


            var mappedBooking = _mapper.Map<Booking>(newbooking);
            // Get the Tour and Hotel if exists

            var tour = await _tourservice.GetTourByID(mappedBooking.TourId);
            var hotel = await _hotelservice.GetHotelById(mappedBooking.HotelId);


            if (hotel == null || tour == null) {

                _response.ErrorMessage = "Invalid Request , " +
                    "Either the Tour destination or Hotel does not exist";
                return BadRequest(_response);
            }

            var totalcost = tour.Price + (mappedBooking.Adults * hotel.AdultPrice * (tour.EndDate - tour.StartDate).TotalDays)
                + (mappedBooking.Kids * hotel.KidsPrice * (tour.EndDate - tour.StartDate).TotalDays);
            mappedBooking.BookingTotal = totalcost;

            var res = _bookingservice.AddBooking(mappedBooking);

            _response.Result = res;
            return Ok(_response);

        }

        [HttpGet("tour/{id}")]

        public async Task<ActionResult<ResponseDTO>>GetTourById(Guid id)
        {
            var tour = await _tourservice.GetTourByID(id);
            if(tour == null)
            {

                _response.ErrorMessage = "Tour Not Found";
                return NotFound(_response);
            }
            _response.Result = tour;
            return Ok(_response);
        }

        [HttpGet("hotel/{id}")]

        public async Task<ActionResult<ResponseDTO>> GetHotelById(Guid id)
        {
            var hotel = await _hotelservice.GetHotelById(id);
            if (hotel == null)
            {

                _response.ErrorMessage = "Hotel Not Found";
                return NotFound(_response);
            }
            _response.Result = hotel;
            return Ok(_response);
        }

        [HttpPut("{Id}")]

        public async Task<ActionResult<ResponseDTO>> ApplyCoupon(Guid Id , string couponcode)
        {

            var booking = await _bookingservice.GetBookingById(Id);
            if (booking == null)
            {
                _response.ErrorMessage = "Booking not found";
                return NotFound(_response);
            }

            var coupon = await _couponservice.GetCouponByCode(couponcode);

            if (coupon == null)
            {
                _response.ErrorMessage = "Coupon not found";
                return NotFound(_response);
            }

            if(coupon.CouponMinAmount <= booking.BookingTotal)
            {
                booking.Discount = coupon.CouponAmount;
                await _bookingservice.UpdateBooking();
                _response.Result = "Coupon Applied";
                return Ok(_response);
            }

            _response.ErrorMessage = $"Booking Total should be greater than {coupon.CouponAmount}";

            return BadRequest(_response);

        }


        [HttpGet("coupon/{code}")]


        public async Task<ActionResult<ResponseDTO>> GetCoupon(string code)
        {

            var coupon = await _couponservice.GetCouponByCode(code);

            if (coupon == null)
            {
                _response.ErrorMessage = "Not Found";
                _response.Issuccess = false;
                return NotFound(_response);
            }

            _response.Result = coupon;
            return Ok(_response);

        }

        [HttpPost("Pay")]

        public async Task<ActionResult<ResponseDTO>> MakePayment(StripeRequestDTO stripeReq) {
        
            var res = await _bookingservice.MakePayments(stripeReq);
            _response.Result = res;
            return Ok(_response);
        
        }

        [HttpPost("validate/{Id}")]

        public async Task<ActionResult<ResponseDTO>> validatePayment(Guid Id)
        {

            var res = await _bookingservice.ValidatePayments(Id);

            if (res)
            {
                _response.Result = res;
                return Ok(_response);
            }

            _response.ErrorMessage = "Payment Failed!";
            return BadRequest(_response);
        }


    }
}
