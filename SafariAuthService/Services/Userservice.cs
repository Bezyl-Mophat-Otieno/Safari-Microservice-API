using AutoMapper;
using Ecommerce.Services;
using Ecommerce.Services.Iservices;
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
        private readonly IJwt _jwtservice;


        public Userservice( IMapper mapper , SafariDBContext dBContext , UserManager<ApplicationUser> usermanager ,RoleManager<IdentityRole> rolemanager , IJwt jwtservice)
        {
            _mapper = mapper;
            _dbContext = dBContext;
            _usermanager = usermanager;
            _rolemanager = rolemanager;
            _jwtservice = jwtservice;
            
        }
        public async Task<bool> AssignUserRoleAsync(string Email, string RoleName)
        {
            try {

                // first check if the user exists

                var user = await _dbContext.Users.Where(x => x.Email.ToLower() == Email.ToLower()). FirstOrDefaultAsync();

                if (user == null)
                {

                    return false;
                }

                // Check if role exists 

                if (!await _rolemanager.RoleExistsAsync(RoleName)) {

                    // Create the role
                    await _rolemanager.CreateAsync(new IdentityRole(RoleName));
                
                }

                // Assign the User the role 

                await _usermanager.AddToRoleAsync(user, RoleName);

                return true;
            
            
            
            }catch(Exception ex)
            {

                return false;
            }
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
                var roles = await  _usermanager.GetRolesAsync(user);
                var token = _jwtservice.GetToken(user,roles);

                var response = new LoginResponseDTO()
                {
                    User = mappeduser,
                    Token = token
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
                    // Check if the role exists 

                    var roleexist = await _rolemanager.RoleExistsAsync(user.Role);

                    if (!roleexist)
                    {
                        // Create the role

                        await _rolemanager.CreateAsync( new IdentityRole(user.Role));

                        // assign the Role 

                        await _usermanager.AddToRoleAsync(mappeduser,user.Role);
                    }

                    await _usermanager.AddToRoleAsync(mappeduser , user.Role);



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
