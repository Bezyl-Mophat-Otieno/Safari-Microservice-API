using System.ComponentModel.DataAnnotations;

namespace CouponService.Data.Dto
{
    public class AddCouponDto
    {
        [Required]
        public string CouponCode { get; set; }

        [Required]
        [Range(100, 2000)]
        public int CouponAmount { get; set; }

        [Required]
        [Range(100, int.MaxValue)]
        public int CouponMinAmount { get; set; }
    }
}

