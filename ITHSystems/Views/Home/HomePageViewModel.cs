using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.DTOs;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Home;

public partial class HomePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<ModuleDto> homeModule;
    

    public HomePageViewModel()
    {
        HomeModule = new(BuildHomeModules.GetHomeModules().OrderBy(x => x.Order));
    }

    
}
