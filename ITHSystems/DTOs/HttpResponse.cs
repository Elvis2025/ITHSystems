using Newtonsoft.Json;

namespace ITHSystems.DTOs;

public record class HttpResponse
{
    [JsonProperty("targetUrl")]
    public string TargetUrl { get; set; } = string.Empty;

    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("error")]
    public ErrorDto Error { get; set; } = new();

    [JsonProperty("unAuthorizedRequest")]
    public bool UnAuthorizedRequest { get; set; }

    [JsonProperty("__abp")]
    public bool Abp { get; set; }
}
