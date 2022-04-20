using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Api.Models;

namespace Weather.Api.Readers
{
    public interface ICsvReader
    {
        Task<IEnumerable<WeatherModel>> ReadCsv<T>(HttpResponseMessage response) where T : ClassMap;
    }
}