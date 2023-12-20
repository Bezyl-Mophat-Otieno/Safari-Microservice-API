using SafariAuthService.Models;

namespace Ecommerce.Services.Iservices
{
    public interface IJwt
    {
        string GetToken(ApplicationUser user , IEnumerable<string>Roles);
    }
}
