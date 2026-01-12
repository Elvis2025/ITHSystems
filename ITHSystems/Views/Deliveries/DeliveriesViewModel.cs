using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.Attributes;
using ITHSystems.DTOs;
using ITHSystems.Services.Delivery;
using ITHSystems.Views.Home;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Deliveries;
[RegisterViewModsel]
public partial class DeliveriesViewModel : BaseViewModel
{
    private readonly IDeliveryService deliveryService;
    [ObservableProperty]
    private ObservableCollection<ModuleDto?> deliveriesModules = new();

    public DeliveriesViewModel(IDeliveryService deliveryService)
    {
        init();
    }

    public void init()
    {
        DeliveriesModules = new(BuildHomeModules.GetDeliveriesModules().OrderBy(x => x.Order));
    }

}
