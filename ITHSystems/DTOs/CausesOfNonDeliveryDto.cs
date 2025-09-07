namespace ITHSystems.DTOs;

public record class CausesOfNonDeliveryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
