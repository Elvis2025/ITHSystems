using System.Text.Json.Serialization;

namespace ITHSystems.DTOs;

public class CustomerDto
{
    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = string.Empty;

    [JsonPropertyName("locators")]
    public List<LocatorDto> Locators { get; set; } = new();

    [JsonPropertyName("identifications")]
    public List<IdentificationDto> Identifications { get; set; } = new();
}
