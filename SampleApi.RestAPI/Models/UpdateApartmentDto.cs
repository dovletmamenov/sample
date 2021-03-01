using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.RestAPI.Models
{
    public class UpdateApartmentDto
    {        
        
        [Required]
        public int LocationId { get; set; }
    }
}
