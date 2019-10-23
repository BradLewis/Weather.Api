using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Data.Models
{
    public class Station
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; }
        public string Province { get; set; }
        public string ClimateId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float? Elevation { get; set; }
        public int FirstYear { get; set; }
        public int LastYear { get; set; }
    }
}
