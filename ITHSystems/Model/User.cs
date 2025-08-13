using ITHSystems.Attributes;

namespace ITHSystems.Model;

[SQLiteEntity]
public class User
{
    public int Id { get; set; } = new();
    public string Name { get; set; } = string.Empty;
}
