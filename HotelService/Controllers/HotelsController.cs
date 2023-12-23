using AutoMapper;
using HotelService.Data.Dto;
using HotelService.Models;
using HotelService.Models.Dto;
using HotelService.Services.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotelservice;
        private readonly IMapper _mapper;
        private readonly ITour _tourservuce;

        private readonly ResponseDTO _response;

        public HotelsController(IHotel hotelservice , IMapper mapper , ITour tourservuce)
        {
            _hotelservice = hotelservice;
            _mapper = mapper;
            _tourservuce = tourservuce;
            _response = new ResponseDTO();
           
        }


        [HttpPost]

        public async Task<ActionResult<ResponseDTO>>AddHotel(AddHotelDTO newHotel)
        {
            var tour = await _tourservuce.GetTourById(newHotel.TourId);
            if(tour == null)
            {
                _response.ErrorMessage = "Tour Does Not Exist";
                return NotFound(_response);
            }

            var mappedHotel = _mapper.Map<Hotel>(newHotel);
            var response = await _hotelservice.AddHotel(mappedHotel);
            _response.Result = response;

            return Ok(_response);
        }


        [HttpGet("{Id}")]

        public async Task<ActionResult<ResponseDTO>> GetHotel(Guid Id) { 
        
        var hotel = await _hotelservice.GetHotelById(Id);
            if(hotel == null)
            {
                _response.ErrorMessage = "Hotel Not Found";
                return NotFound(_response);
            }

            _response.Result = hotel;
            return Ok(_response);
    
        
        }

        [HttpGet("tour/location/{Id}")]
        public async Task<ActionResult<ResponseDTO>> GetHotels(Guid Id)
        {
            var hotels =await _hotelservice.GetHotelsByTourLocation(Id);
            if(hotels.Count() == 0)
            {
                _response.ErrorMessage = "No hotels in that Tour Location";
                return NotFound(_response);
            }
            _response.Result = hotels;

            return Ok(_response);

        }

        }
    }
