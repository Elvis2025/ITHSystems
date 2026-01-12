using Newtonsoft.Json;

namespace ITHSystems.DTOs;

public class ErrorDto
{
    [JsonProperty("code")]
    public int? Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("details")]
    public string Details { get; set; } = string.Empty;
}
