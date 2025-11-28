using Newtonsoft.Json;

namespace ITHSystems.DTOs;

public class ResultDto<T> 
{
    [JsonProperty("totalCount")]
    public int TotalCount => Items.Count;

    [JsonProperty("items")]
    public List<T> Items { get; set; } = new();
}
