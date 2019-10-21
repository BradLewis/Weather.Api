using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Client.Models
{
    public class EnvrionmentCanadaEntry
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string StationName { get; set; }
        public int ClimateId { get; set; }
        public DateTime DateTime { get; set; }
        public double Temperature { get; set; }
        public string TemperatureFlag { get; set; }
        public double DewPointTemperature { get; set; }
        public string DewPointTemperatureFlag { get; set; }
        public double RelativeHumidity { get; set; }
        public string RelativeHumidityFlag { get; set; }
        public double WindDirection { get; set; }
        public string WindDirectionFlag { get; set; }
        public double WindSpeed { get; set; }
        public string WindSpeedFlag { get; set; }
        public double Visibility { get; set; }
        public string VisibilityFlag { get; set; }
        public double Pressure { get; set; }
        public string PressureFlag { get; set; }
        public double Humidex { get; set; }
        public string HumidexFlag { get; set; }
        public double WindChill { get; set; }
        public string WindChillFlag { get; set; }
        public string Weather { get; set; }
    }
}
