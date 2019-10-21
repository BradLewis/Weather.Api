using CsvHelper.Configuration;
using Weather.Client.Models;

namespace Weather.Client.Maps
{
    public class EnvironmentCanadaMap : ClassMap<WeatherModel>
    {
        public EnvironmentCanadaMap()
        {
            Map(x => x.Longitude).Name("Longitude (x)");
            Map(x => x.Latitude).Name("Latitude (y)");
            Map(x => x.StationName).Name("Station Name");
            Map(x => x.DateTime).Name("Date/Time");
            Map(x => x.Temperature).Name("Temp (°C)");
            Map(x => x.DewPointTemperature).Name("Dew Point Temp (°C)");
            Map(x => x.RelativeHumidity).Name("Rel Hum (%)");
            Map(x => x.WindDirection).Name("Wind Dir (10s deg)");
            Map(x => x.WindSpeed).Name("Wind Spd (km/h)");
            Map(x => x.Visibility).Name("Visibility (km)");
            Map(x => x.Pressure).Name("Stn Press (kPa)");
            Map(x => x.Humidex).Name("Hmdx");
            Map(x => x.WindChill).Name("Wind Chill");
            Map(x => x.Weather).Name("Weather");
        }
    }
}
