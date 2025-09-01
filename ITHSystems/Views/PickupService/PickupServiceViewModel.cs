using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.DTOs;
using ITHSystems.Views.Home;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.PickupService;

public partial class PickupServiceViewModel : BaseViewModel
{

    [ObservableProperty]
    private ObservableCollection<ModuleDTO?> pickupServiceModules;

    public PickupServiceViewModel()
    {
        PickupServiceModules = new(BuildHomeModules.GetPickupModules().OrderBy(x => x.Order));
    }
}
