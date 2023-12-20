using SafariAuthService.Data.Dto;

namespace SafariAuthService.Services.Iservice
{
    public interface IUser
    {
        Task<string> RegisterUserAsync(RegisterUserDTO user);

        Task<LoginResponseDTO> LoginUserAsync(LoginRequestDTO userlogin);


        Task<bool> AssignUserRoleAsync(string Email, string RoleName);
    }
}
