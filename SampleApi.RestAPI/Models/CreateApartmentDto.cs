using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.RestAPI.Models
{
    public class CreateApartmentDto
    {
        [Required]
        [Range(10, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int RentalPrice { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int NumberOfBeds { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public float SquareMeters { get; set; }
        
        [Required]
        public int LocationId { get; set; }
    }
}
