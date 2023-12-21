using AutoMapper;
using CouponService.Data.Dto;
using CouponService.Models;

namespace CouponService.Profiles
{
    public class CouponProfile:Profile
    {
        public CouponProfile()
        {
            CreateMap<Coupon , AddCouponDto>().ReverseMap();
            CreateMap<Coupon,UpdateCouponDTO>().ReverseMap();

           
        }
    }
}
