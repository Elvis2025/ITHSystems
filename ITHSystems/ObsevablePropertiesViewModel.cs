using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.DTOs;
using ITHSystems.UsesCases.IconFonts;
using System.Collections.ObjectModel;


namespace ITHSystems.Views;

public abstract partial class ObsevablePropertiesViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;
    [ObservableProperty]
    private bool isPassword = true;
    [ObservableProperty]
    private string passwordEye = IconsTwoTone.Visibility_off;
    [ObservableProperty]
    private UserDto currentUser = new();
    [ObservableProperty]
    private ModuleDto? currentModule;
    private bool IsNotBusy => !IsBusy;
    [ObservableProperty]
    private UserDto userDTO = new();
    [ObservableProperty]
    private ObservableCollection<OrdersDto> orders = new();


}
