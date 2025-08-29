using ITHSystems.Attributes;

namespace ITHSystems.Model;

[SQLiteEntity]
public class User : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
}
