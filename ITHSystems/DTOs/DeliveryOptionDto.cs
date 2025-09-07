namespace ITHSystems.DTOs;

public record class DeliveryOptionDto
{
    public Enums.Receiver Id {get; set; }
    public string Name { get; set; } = string.Empty;

}
