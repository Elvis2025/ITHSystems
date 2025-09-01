using ITHSystems.Attributes;

namespace ITHSystems.DTOs;
[AutoMap(typeof(Model.User))]
public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
