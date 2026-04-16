using ITHSystems.Attributes;

namespace ITHSystems.Model;

[SQLiteEntity]
public class ProductsCartList : Entity<int>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Tax { get; set; } = new();
    public decimal Price { get; set; } = new();
    public string Image { get; set; } = string.Empty;
}
