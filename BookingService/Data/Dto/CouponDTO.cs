namespace BookingService.Models.Dto
{
    public class CouponDTO
    {
        public Guid Id { get; set; }

        public string CouponCode { get; set; } = String.Empty;
        public int CouponAmount { get; set; }
        public int CouponMinAmount { get; set; }

    }
}
