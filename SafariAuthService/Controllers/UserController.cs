using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafariAuthService.Data.Dto;
using SafariAuthService.Services.Iservice;

namespace SafariAuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userservice;

        private readonly ResponseDTO _response;

        public UserController(IUser userservice)
        {
            _userservice = userservice;
            _response = new ResponseDTO();

        }


        [HttpPost("register")]

        public async Task<ActionResult<ResponseDTO>> RegisterUser(RegisterUserDTO user)
        {

            var res = await _userservice.RegisterUserAsync(user);

            if (string.IsNullOrEmpty(res))
            {
                _response.Result = "User Registered Successfully";
                return Created("", _response);
            }

            _response.ErrorMessage = res;
            _response.Issuccess = false;
            return BadRequest(_response);

        }

        [HttpPost("login")]

        public async Task<ActionResult<string>> Login( LoginRequestDTO user ) {

            var response = await _userservice.LoginUserAsync(user);

            if(response.User == null) { 
            
                return Unauthorized("Invalid Credentials");
            
            }

            return Ok(response.Token);
        
        
        }



        [HttpPost("assignrole")]

        public async Task<ActionResult<ResponseDTO>> AssignRole(AssignRoleDTO assignRoleDTO)
        {

            var res = await _userservice.AssignUserRoleAsync(assignRoleDTO.Email, assignRoleDTO.Role) ;

            if (res)
            {
                _response.Result = res;
                return Ok(_response);
            }

            _response.ErrorMessage = "User failed to be assigned the Role";
            _response.Result = res;
            _response.Issuccess = false;
            return BadRequest(_response);

        }
    }
}
