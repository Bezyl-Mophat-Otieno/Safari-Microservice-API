using CouponService.Models;

namespace CouponService.Services.Iservices
{
    public interface Icoupon
    {
        Task<List<Coupon>> GetAllCoupons();
        Task<Coupon> GetCoupon(Guid Id
            );

        Task<string> AddCoupon(Coupon coupon);
        Task<string> UpdateCoupon(Coupon updated);
        Task<string> DeleteCoupon(Coupon coupon);


    }
}
