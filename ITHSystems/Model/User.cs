using ITHSystems.Attributes;
using ITHSystems.Repositories.SQLite;

namespace ITHSystems.Model;

[SQLiteEntity]
public class User : Entity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
