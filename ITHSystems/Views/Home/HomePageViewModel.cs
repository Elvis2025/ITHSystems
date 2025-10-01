using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.Attributes;
using ITHSystems.DTOs;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Home;
[RegisterViewModsel]
public partial class HomePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<ModuleDto> homeModule;
    

    public HomePageViewModel()
    {
        HomeModule = new(BuildHomeModules.GetHomeModules().OrderBy(x => x.Order));
    }

    
}
