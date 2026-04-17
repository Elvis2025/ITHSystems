using ITHSystems.Attributes;

namespace ITHSystems.Model;
[SQLiteEntity]
public class BaseUrl : Entity<int>
{
    public string Url { get; set; } = string.Empty;
}
