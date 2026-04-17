using ITHSystems.Attributes;

namespace ITHSystems.Model;

[SQLiteEntity]
public class Tenant : Entity<int>
{
    public string TenancyName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public int TenantId { get; set; } = new();
   

}
