using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.UsesCases.IconFonts;
using ITHSystems.Views.Login.DTO;

namespace ITHSystems.Views;

public abstract partial class ObsevablePropertiesViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    public bool isBusy;
    [ObservableProperty]
    public bool isPassword = true;
    [ObservableProperty]
    public string passwordEye = IconsTwoTone.Visibility_off;
    [ObservableProperty]
    public UserDTO currentUser = new();

    public bool IsNotBusy => !IsBusy;
    [ObservableProperty]
    public UserDTO userDTO = new();
}
