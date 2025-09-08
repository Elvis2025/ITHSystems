namespace ITHSystems.DTOs;

public record class OptionsDelivery
{
    public Enums.Receiver Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}
