using System.ComponentModel.DataAnnotations;

namespace CouponService.Data.Dto
{
    public class UpdateCouponDTO
    {
        public string? CouponCode { get; set; }

        public int? CouponAmount { get; set; }

        public int? CouponMinAmount { get; set; }
    }
}
