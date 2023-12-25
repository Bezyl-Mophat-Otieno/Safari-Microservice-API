using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursService.Data.Dto;
using ToursService.Models;
using ToursService.Services.Iservice;

namespace ToursService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {

        private readonly ITour _tourservice;
        private readonly ResponseDTO _response;
        private readonly IMapper _mapper;
        public ToursController(ITour tourservice , IMapper mappper)
        {
            _tourservice = tourservice;
            _response = new ResponseDTO();
            _mapper = mappper;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]

        public async Task<ActionResult<ResponseDTO>> AddTour(AddTourDTO tour)
        {
            var mappedtour = _mapper.Map<Tour>(tour);

            var res = await _tourservice.AddTour(mappedtour);

            _response.Result = res;
            return Ok(_response);
        }


        [HttpGet]

        public async Task<ActionResult<ResponseDTO>> GetAllTours()
        {
            var tours = await _tourservice.GetAllToursAsync();

            _response.Result = tours;
            return Ok(_response);

        }

        [HttpGet("{Id}")]

        public async Task<ActionResult<ResponseDTO>> GetTourById(Guid Id)
        {
            var tour = await _tourservice.GetTourAsync(Id);

            if(tour == null)
            {
                _response.ErrorMessage = "Tour Destination Not Found";
                return NotFound(_response);
            }
            _response.Result = tour;
            return Ok(_response);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]


        public async Task<ActionResult> DeleteTour(Guid Id)
        {
            var tour = await _tourservice.GetTourAsync(Id);
            if (tour == null)
            {
                _response.ErrorMessage = "Failed to Find the Tour with that ID";
                return NotFound(_response);
            }
            var isSuccess = await _tourservice.DeleteTourAsync(tour);

            if (isSuccess)
            {
                return NoContent();
            }
            _response.ErrorMessage = "Operation Failed";
            return StatusCode(500, _response);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResponseDTO>> UpdateTour(AddTourDTO updatedTour, Guid Id)
        {

            var existingTour = await _tourservice.GetTourAsync(Id);
            if (existingTour == null)
            {
                _response.ErrorMessage = "Tour with such an Id not Found";
                return NotFound(_response);
            }

            var mappedTour = _mapper.Map(updatedTour, existingTour);

            _response.Result = mappedTour;
            await _tourservice.UpdateTourAsync();

            return Ok(_response);

        }

    }
}
