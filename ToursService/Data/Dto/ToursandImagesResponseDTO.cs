namespace ToursService.Data.Dto
{
    public class ToursandImagesResponseDTO
    {
        public Guid Id { get; set;}

        public string SafariName { get; set; } = string.Empty;

        public string SafariDescription { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<AddTourImageDTO> TourImages { get; set; }
        public int Price { get; set; }
    }
}
