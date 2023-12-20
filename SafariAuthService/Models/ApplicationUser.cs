using Microsoft.AspNetCore.Identity;

namespace SafariAuthService.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
