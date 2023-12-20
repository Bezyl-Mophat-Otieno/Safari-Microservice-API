using System.ComponentModel.DataAnnotations;

namespace CouponService.Models
{
    public class Coupon
    {
        [Key]
        public Guid Id { get; set; }

        public string CouponCode { get; set; }

        public int CouponAmount {  get; set; }
        

        public int CouponMinAmount { get; set; }
    }
}
