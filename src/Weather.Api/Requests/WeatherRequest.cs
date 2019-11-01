using System;

namespace Weather.Api.Requests
{
    public class WeatherRequest
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
