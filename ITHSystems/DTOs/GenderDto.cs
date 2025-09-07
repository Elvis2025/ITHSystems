using ITHSystems.Enums;

namespace ITHSystems.DTOs;

public record class GenderDto
{
    public Gender Id { get; set; }
    public string Description { get; set; } = string.Empty;
}
