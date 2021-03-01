using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.RestAPI.Models
{
    public class CreateLocationDto
    {
        public string Flat { get; set; }
        [Required]
        public string Building { get; set; }
        [Required]
        public string StreetName { get; set; }
    }
}
