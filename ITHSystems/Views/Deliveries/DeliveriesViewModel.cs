using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.DTOs;
using ITHSystems.Views.Home;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Deliveries;

public partial class DeliveriesViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<ModuleDto?> deliveriesModules;

    public DeliveriesViewModel()
    {
        DeliveriesModules = new(BuildHomeModules.GetDeliveriesModules().OrderBy(x => x.Order));
    }


}
