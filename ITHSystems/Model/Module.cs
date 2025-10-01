using ITHSystems.Repositories.SQLite;

namespace ITHSystems.Model;

public class Module : Entity<int>
{
    public string Title { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string FontFamaly { get; set; } = string.Empty;
}
