using CouponService.Models;

namespace CouponService.Services.Iservices
{
    public interface Icoupon
    {
        Task<List<Coupon>> GetAllCoupons();
        Task<Coupon> GetCoupon(Guid Id);
        Task<Coupon> GetCouponByCode(string code);

        Task<string> AddCoupon(Coupon coupon);
        Task<string> UpdateCoupon();
        Task<string> DeleteCoupon(Coupon coupon);


    }
}
