using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Services.General;
using ITHSystems.Services.Login;
using ITHSystems.Views.Home;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Deliveries;
[RegisterViewModsel]
public partial class DeliveriesViewModel : BaseViewModel
{
    private readonly ILoginService loginService;
    private readonly IPreferenceService preferenceService;
    [ObservableProperty]
    private ObservableCollection<ModuleDto?> deliveriesModules = new();

    public DeliveriesViewModel(ILoginService loginService, IPreferenceService preferenceService)
    {
        this.loginService = loginService;
        this.preferenceService = preferenceService;
        init();
    }

    public void init()
    {
        Task.Run(async () =>
        {
            IsBusy = true;
            var orders = await loginService.GetOrder(new() { JWT = preferenceService.Get(IBS.JWT) });
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
