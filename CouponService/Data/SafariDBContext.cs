using CouponService.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponService.Data
{
    public class SafariDBContext:DbContext
    {
        public SafariDBContext(DbContextOptions<SafariDBContext> options):base(options) { }
        public DbSet<Coupon> Coupons { get; set; }

    }
}
