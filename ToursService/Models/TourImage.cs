using System.ComponentModel.DataAnnotations.Schema;

namespace ToursService.Models
{
    public  class TourImage
    {

        public Guid Id { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
        [ForeignKey("TourId")]
        public Tour tour { get; set; } = default!;

        public Guid TourId { get; set; }
    }
}