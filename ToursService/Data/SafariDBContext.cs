using Microsoft.EntityFrameworkCore;
using ToursService.Models;

namespace ToursService.Data
{
    public class SafariDBContext:DbContext
    {
        public SafariDBContext(DbContextOptions<SafariDBContext>options):base(options) { }

        public DbSet<Tour> Tours { get; set; }

        public DbSet<TourImage> Images { get; set; }
     
    }
}
