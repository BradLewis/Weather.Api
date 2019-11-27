using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Client.Models;

namespace Weather.Client
{
    public interface ICsvReader
    {
        Task<IEnumerable<WeatherModel>> ReadCsv<T>(HttpResponseMessage response) where T : ClassMap;
    }
}