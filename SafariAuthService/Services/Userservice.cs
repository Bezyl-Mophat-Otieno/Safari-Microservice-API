using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafariAuthService.Data;
using SafariAuthService.Data.Dto;
using SafariAuthService.Models;
using SafariAuthService.Services.Iservice;

namespace SafariAuthService.Services
{
    public class Userservice : IUser
    {

        private readonly SafariDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;


        public Userservice( IMapper mapper , SafariDBContext dBContext , UserManager<ApplicationUser> usermanager ,RoleManager<IdentityRole> rolemanager)
        {
            _mapper = mapper;
            _dbContext = dBContext;
            _usermanager = usermanager;
            _rolemanager = rolemanager;
            
        }
        public Task<bool> AssignUserRoleAsync(string Email, string RoleName)
        {
            throw new NotImplementedException();
        }

        public  async Task<LoginResponseDTO> LoginUserAsync(LoginRequestDTO userlogin)
        {
            try {

                // Check if user exists 

                var user = await _dbContext.Users.Where(x=>x.UserName.ToLower() == userlogin.UserName).FirstOrDefaultAsync();


               

                if (user == null)
                {
                    return new LoginResponseDTO();
                }

                // Verify the password 

                var isvalid =await  _usermanager.CheckPasswordAsync(user , userlogin.Password);

                if (!isvalid)
                {
                    return new LoginResponseDTO();

                }

                var mappeduser = _mapper.Map<UserDTO>(user);

                var response = new LoginResponseDTO()
                {
                    User = mappeduser,
                    Token = "coming soon"
                };


                return response;
                   
            
            } catch (Exception ex) {

                return new LoginResponseDTO();
            
            }
        }

        public async Task<string> RegisterUserAsync(RegisterUserDTO user)
        {
            try {
                var mappeduser = _mapper.Map<ApplicationUser>(user);

               var result = await _usermanager.CreateAsync(mappeduser,user.Password);

                if (result.Succeeded)
                {
                    return string.Empty;
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            
            }catch(Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
