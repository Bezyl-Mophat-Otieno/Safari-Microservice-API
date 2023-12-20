using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SafariAuthService.Models;

namespace SafariAuthService.Data
{
    public class SafariDBContext:IdentityDbContext<ApplicationUser>
    {
        public SafariDBContext(DbContextOptions<SafariDBContext> options) : base(options) { }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}
