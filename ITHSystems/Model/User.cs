using ITHSystems.Attributes;
using SQLite;

namespace ITHSystems.Model;

[SQLiteEntity]
public class User : Entity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [Indexed(Name = "UX_User_UserNamePasswordJwt", Order = 1, Unique = true)]
    public string UserName { get; set; } = string.Empty;
    [Indexed(Name = "UX_User_UserNamePasswordJwt", Order = 2, Unique = true)]
    public string Password { get; set; } = string.Empty;
    public string Pin { get; set; } = string.Empty;
    [Indexed(Name = "UX_User_UserNamePasswordJwt", Order = 3, Unique = true)]
    public string JWT { get; set; } = string.Empty;
    public DateTime Issued { get; set; }
    public DateTime Expires { get; set; }
}
