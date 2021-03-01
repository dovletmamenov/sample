using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SampleApi.Data.Entities
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        public int RentalPrice { get; set; }

        public int NumberOfBeds { get; set; }

        public float SquareMeters { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

    }
}
