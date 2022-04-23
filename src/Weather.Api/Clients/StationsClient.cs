using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Weather.Api.Models;

namespace Weather.Api.Clients;

public class StationsClient : IStationsClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly EndpointSettings _endpointSettings;
    private readonly ILogger<StationsClient> _logger;

    public StationsClient(IHttpClientFactory httpClientFactory, IOptions<EndpointSettings> endpointSettings, ILogger<StationsClient> logger)
    {
        _httpClientFactory = httpClientFactory;
        _endpointSettings = endpointSettings.Value;
        _logger = logger;
    }

    public async Task<IEnumerable<Station>> GetByName(string name)
    {
        _logger.LogInformation("Sending request to stations api for name {name}", name);
        using var client = _httpClientFactory.CreateClient("StationsClient");
        var endpoint = $"{_endpointSettings.StationsApi}/name/{name}";
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        var response = await client.SendAsync(request);
        _logger.LogInformation("Response received for request {name} with status code {statusCode}", name, response.StatusCode);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Station>>();
    }

    public async Task<Station> GetById(int id)
    {
        _logger.LogInformation("Sending request to stations api for id {id}", id);
        using var client = _httpClientFactory.CreateClient("StationsClient");
        var endpoint = $"{_endpointSettings.StationsApi}/id/{id}";
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        var response = await client.SendAsync(request);
        _logger.LogInformation("Response received for request {id} with status code {statusCode}", id, response.StatusCode);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Station>();
    }
}

