using ITHSystems.Attributes;

namespace ITHSystems.DTOs;

[AutoMap(typeof(Model.Module))]
public record class ModuleDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string FontFamaly { get; set; } = string.Empty;
    public Enums.Modules Modules { get; set; } = Enums.Modules.NONE;
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int Order { get; set; }
    public int Badges { get; set; } = -1;
    public bool EnableBadges { get; set; }
    public bool IsVisible => !IsDeleted;
    public bool Enable => Badges > 0;
    public Color BGColor => EnableBadges ? 
                                Enable ?
                                    Color.FromArgb("#FFFFFF")
                                    : Color.FromArgb("#6E6E6E")
                                : Color.FromArgb("#FFFFFF");
}
