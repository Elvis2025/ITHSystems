using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.Attributes;
using ITHSystems.DTOs;
using ITHSystems.Views.Home;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.PickupService;
[RegisterViewModsel]
public partial class PickupServiceViewModel : BaseViewModel
{

    [ObservableProperty]
    private ObservableCollection<ModuleDto?> pickupServiceModules;

    public PickupServiceViewModel()
    {
        PickupServiceModules = new(BuildHomeModules.GetPickupModules().OrderBy(x => x.Order));
    }
}
