using System.ComponentModel.DataAnnotations;

namespace SafariAuthService.Data.Dto
{
    public class RegisterUserDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]

        public string Email { get; set; } = string.Empty;

        [Required]

        public string Password { get; set; } = string.Empty;

        [Required]

        public string PhoneNumber {  get; set; } = string.Empty;

        public string? Role { get; set; } = "User";
    }
}
