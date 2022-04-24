using ISSLocation.Models;
using Newtonsoft.Json;

namespace ISSLocation.Services;

public class IssLocationService : IIssLocationService
{
    private readonly HttpClient _httpClient;
    public IssLocationService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["BaseUrl"]);
    }
    public async Task<IssLocation> GetLocation()
    {
        var result = await _httpClient.GetStringAsync("/iss-now.json");
        if (!string.IsNullOrWhiteSpace(result))
        {
            return JsonConvert.DeserializeObject<IssLocation>(result);
        }
        
        throw new InvalidDataException("Received Json is not valid");
    }
}