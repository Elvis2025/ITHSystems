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
        this.deliveryService = deliveryService;
        init();
    }

    public void init()
    {
        Task.Run(async () =>
        {
            IsBusy = true;
            var orders = await deliveryService.GetOrder();
            Orders.Clear();
            DeliveriesModules.Clear();
            Orders = new(orders.Data.Items);
            var modules = BuildHomeModules.GetDeliveriesModules().OrderBy(x => x.Order);

            foreach (var item in modules)
            {
                if (item.Modules == Enums.Modules.PENDINGDELIVERIES)
                {
                    item.Badges = orders.Data.TotalCount;
                }
                DeliveriesModules.Add(item);
            }
            IsBusy = false;
        });
    }

}
