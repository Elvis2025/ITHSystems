using Newtonsoft.Json;

namespace ITHSystems.DTOs;

public class ResponseDto<T>
{
    [JsonProperty("result")]
    public ResultDto<T> Data { get; set; } = new();

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
