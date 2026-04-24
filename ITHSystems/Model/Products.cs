using ITHSystems.Attributes;

namespace ITHSystems.Model;

[SQLiteEntity]
public class Products : Entity<int>
{
    public string Barcode { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Tax { get; set; } = new();
    public decimal Cost { get; set; } = new();
    public decimal Price { get; set; } = new();
    public int ImageId { get; set; } = new();
    public string ImageName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}
