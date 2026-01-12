namespace ITHSystems.DTOs;

public sealed record class UserAuthenticationDto : HttpResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime Issued { get; set; }
    public DateTime Expires { get; set; }
    public bool TokenExpired => DateTime.UtcNow >= Expires.AddMinutes(-5);
}
