namespace HotelService.Models.Dto
{
    public class AddHotelDTO
    {
        public string Name { get; set; }

        public Guid TourId { get; set; }

        public int AdultPrice { get; set; }

        public int ChildPrice { get; set; }
    }
}
