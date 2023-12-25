namespace BookingService.Data.Dto
{
    public class AddBookingDTO
    {
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public double BookingTotal { get; set; }
        public int Adults { get; set; }

        public int Kids { get; set; }

        public Guid TourId { get; set; }

        public Guid HotelId { get; set; }
    }
}
