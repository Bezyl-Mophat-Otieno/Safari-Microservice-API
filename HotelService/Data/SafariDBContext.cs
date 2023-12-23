using HotelService.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Data
{
    public class SafariDBContext:DbContext
    {
        public SafariDBContext(DbContextOptions<SafariDBContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
       
    }
}
