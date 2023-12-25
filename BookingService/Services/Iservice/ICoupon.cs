using BookingService.Models.Dto;

namespace BookingService.Services.Iservice
{
    public interface ICoupon
    {
        Task<CouponDTO> GetCouponByCode(string code);
    }
}
