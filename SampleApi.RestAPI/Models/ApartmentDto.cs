using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.RestAPI.Models
{
    public class ApartmentDto
    {
       
        public int Id { get; set; }
        
        public int RentalPrice { get; set; }

        
        public int NumberOfBeds { get; set; }

        
        public float SquareMeters { get; set; }

        public LocationDto Location { get; set; }
    }
}
