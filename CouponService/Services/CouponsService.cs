using CouponService.Data;
using CouponService.Models;
using CouponService.Services.Iservices;
using Microsoft.EntityFrameworkCore;

namespace CouponService.Services
{
    public class CouponsService : Icoupon
    {

        private readonly SafariDBContext _dbContext;

        public CouponsService( SafariDBContext dbContext)
        {

            _dbContext = dbContext;
            
        }
        public async Task<string> AddCoupon(Coupon coupon)
        {
            try {
                await  _dbContext.AddAsync(coupon);
                await _dbContext.SaveChangesAsync();

                return "Coupon Added";
            
            
            }catch (Exception ex)
            {
                return ex.Message;


            }
        }

        public async Task<string> DeleteCoupon(Coupon coupon)
        {
            try
            {

                _dbContext.Remove(coupon);
                await _dbContext.SaveChangesAsync();
                return "Coupon deleted successfully";



            }
            catch (Exception ex)
            {

                return ex.Message;


            }
        }

        public async Task<List<Coupon>> GetAllCoupons()
        {
            try
            {

                return await _dbContext.Coupons.ToListAsync();

            }
            catch (Exception ex)
            {

                return new List<Coupon>(); 

            }
        }

        public async Task<Coupon> GetCoupon(Guid Id)
        {
            try
            {

                var coupon = await _dbContext.Coupons.FindAsync(Id);

                return coupon;


            }
            catch (Exception ex)
            {

                return null;


            }
        }

        public async Task<string> UpdateCoupon(Coupon updated)
        {
            try
            {
                     _dbContext.Coupons.Update(updated);
                await _dbContext.SaveChangesAsync();
                return "Updated";

            }
            catch (Exception ex)
            {

                return "Update Failed";


            }
        }
    }
}
