using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SampleApi.Data.Entities
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Flat { get; set; }
        [Required]
        public string Building { get; set; }
        [Required]
        public string StreetName { get; set; }
    }
}
