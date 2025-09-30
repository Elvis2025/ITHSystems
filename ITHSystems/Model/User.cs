using ITHSystems.Attributes;
using ITHSystems.Repositories.SQLite;

namespace ITHSystems.Model;

[SQLiteEntity]
public class User : Entity<int>
{
    public string Name { get; set; } = string.Empty;
}
