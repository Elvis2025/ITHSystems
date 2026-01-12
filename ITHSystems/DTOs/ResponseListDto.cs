using Newtonsoft.Json;

namespace ITHSystems.DTOs;

public sealed record class ResponseListDto<T> : HttpResponse
{
    [JsonProperty("result")]
    public ResultDto<T> Data { get; set; } = new();
}
