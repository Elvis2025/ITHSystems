using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.DTOs;
using ITHSystems.UsesCases.IconFonts;


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
    [ObservableProperty]
    public ModuleDTO? currentModule;
    public bool IsNotBusy => !IsBusy;
    [ObservableProperty]
    public UserDTO userDTO = new();
}
