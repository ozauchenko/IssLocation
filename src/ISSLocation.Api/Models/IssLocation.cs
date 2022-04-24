using Newtonsoft.Json;

namespace ISSLocation.Models;

public class IssLocation
{
    public string Message { get; set; }
    public long Timestamp { get; set; }
    [JsonProperty("iss_position")]
    public Position Position { get; set; }
}