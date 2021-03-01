using System;
namespace SampleApi.RestAPI.Models { 
    public class LocationDto
    {
        public int Id { get; set; }

        public string Flat { get; set; }

        public string Building { get; set; }

        public string StreetName { get; set; }
    }
}
