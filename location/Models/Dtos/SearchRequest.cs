using System.ComponentModel.DataAnnotations;

namespace location.Models.Dtos
{
    public class SearchRequest
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double SearchArea { get; set; }
    }
}