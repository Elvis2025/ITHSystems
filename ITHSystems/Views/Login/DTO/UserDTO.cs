using ITHSystems.Attributes;
using ITHSystems.Extensions;
using ITHSystems.Model;

namespace ITHSystems.Views.Login.DTO;

[AutoMap(typeof(User))]
public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public User User => this.Map<User>();
}
