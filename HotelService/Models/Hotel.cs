namespace HotelService.Models
{
    public class Hotel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid TourId { get; set; }

        public int AdultPrice { get; set; }

        public int ChildPrice { get; set; }
    }
}
