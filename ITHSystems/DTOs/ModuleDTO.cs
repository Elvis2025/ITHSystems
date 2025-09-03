using ITHSystems.Attributes;

namespace ITHSystems.DTOs;

[AutoMap(typeof(Model.Module))]
public record class ModuleDto
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
    public Color BGColor
    {
        get
        {
            if (EnableBadges)
            {
                if (Enable)
                {
                    return Color.FromArgb("#FFFFFF");
                }
                else
                {
                    return Color.FromArgb("#6E6E6E");
                }
            }
            else
            {
                return Color.FromArgb("#FFFFFF");
            }
        }
    }
}
