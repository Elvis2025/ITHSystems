using ITHSystems.Views.Home;

namespace ITHSystems.UnitTests.src.HomeTest;

public class HomeViewModel
{
    [Fact]
    public void HomeViewModelTest()
    {
        var buildHomeModules = BuildHomeModules.GetHomeModules();
        var homeViewModel = new HomePageViewModel();
        Assert.NotNull(buildHomeModules);
        Assert.NotNull(homeViewModel);
        Assert.IsType<HomePageViewModel>(homeViewModel);
    }
}
