using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data
{
    public class SafariDBContext:DbContext
    {
        public SafariDBContext(DbContextOptions<SafariDBContext>options):base(options) { }

        public DbSet<Booking> Bookings { get; set; }


     
    }
}
