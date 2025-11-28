using ITHSystems.Attributes;

namespace ITHSystems.DTOs;
[AutoMap(typeof(Model.User))]
public record class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Pin { get; set; } = string.Empty;
    public string JWT { get; set; } = string.Empty;
    public DateTime Issued { get; set; }
    public DateTime Expires { get; set; }
    public bool TokenExpired => DateTime.UtcNow >= Expires.AddMinutes(-5);
    public bool IsAboutToExpire()
    {
        var now = DateTime.UtcNow;
        var threshold = Expires.AddMinutes(-5);

        return now >= threshold;
    }
}
