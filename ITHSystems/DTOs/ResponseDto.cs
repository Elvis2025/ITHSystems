using Newtonsoft.Json;

namespace ITHSystems.DTOs;

public sealed record class ResponseDto<T> : HttpResponse
{
    [JsonProperty("result")]
    public T Result { get; set; } = default!;
}
