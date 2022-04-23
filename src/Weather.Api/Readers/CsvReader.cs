using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Api.Models;

namespace Weather.Api.Readers;

public class CsvReader : ICsvReader
{
    public async Task<IEnumerable<WeatherModel>> ReadCsv<T>(HttpResponseMessage response) where T : ClassMap
    {
        var csvReaderConfiguration = new CsvConfiguration(cultureInfo: CultureInfo.InvariantCulture);
        csvReaderConfiguration.MissingFieldFound = null;
        using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);
        using var csvReader = new CsvHelper.CsvReader(reader, csvReaderConfiguration);
        csvReader.Context.RegisterClassMap<T>();
        return csvReader.GetRecords<WeatherModel>().ToList();
    }
}
