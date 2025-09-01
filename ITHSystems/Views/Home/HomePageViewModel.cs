using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.DTOs;
using ITHSystems.Views.Deliveries;
using ITHSystems.Views.PickupService;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Home;

public partial class HomePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<ModuleDTO> homeModule;
    

    public HomePageViewModel()
    {
        HomeModule = new(BuildHomeModules.GetHomeModules().OrderBy(x => x.Order));
    }

    
}
