using System;

namespace Weather.Api.Models
{
    public class WeatherModel
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string StationName { get; set; }
        public DateTime DateTime { get; set; }
        public double? Temperature { get; set; }
        public double? DewPointTemperature { get; set; }
        public double? RelativeHumidity { get; set; }
        public double? WindDirection { get; set; }
        public double? WindSpeed { get; set; }
        public double? Visibility { get; set; }
        public double? Pressure { get; set; }
        public double? Humidex { get; set; }
        public double? WindChill { get; set; }
        public string Weather { get; set; }
    }
}