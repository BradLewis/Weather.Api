using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Api.Models;

namespace Weather.Api.Readers
{
    public class CsvReader : ICsvReader
    {
        public async Task<IEnumerable<WeatherModel>> ReadCsv<T>(HttpResponseMessage response) where T : ClassMap
        {
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var csvReader = new CsvHelper.CsvReader(reader))
            {
                csvReader.Configuration.RegisterClassMap<T>();
                csvReader.Configuration.MissingFieldFound = null;
                return csvReader.GetRecords<WeatherModel>().ToList();
            }
        }
    }
}