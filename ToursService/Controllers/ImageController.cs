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
    public class ImageController : ControllerBase
    {

        private readonly Iimage _imageservice;
        private readonly ResponseDTO _response;
        private readonly IMapper _mapper;
        private readonly ITour _tourservice;


        public ImageController(Iimage imagservice , IMapper mapper , ITour tourservice)
        {
            _imageservice = imagservice;
            _response = new ResponseDTO();
            _mapper = mapper;
            _tourservice = tourservice;

        }

        [HttpPost("{Id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResponseDTO>> AddTourImage(Guid Id ,AddTourImageDTO image ) {


            var tour = await _tourservice.GetTourAsync( Id );

            if (tour == null)
            {
                _response.Result = "Tour Not Found";
                return NotFound(_response);
            }
            var mappedimage = _mapper.Map<TourImage>(image);

            var response = await _imageservice.AddImage(Id, mappedimage);
        
            _response.Result = response;

            return Created("",_response);
        
        }
    }
}
