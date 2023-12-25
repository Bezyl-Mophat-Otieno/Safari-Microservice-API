﻿namespace BookingService.Models.Dto
{
    public class HotelDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid TourId { get; set; }
        public int AdultPrice { get; set; }

        public int KidsPrice { get; set; }
    }
}
