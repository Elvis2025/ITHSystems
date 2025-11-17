using ITHSystems.DTOs;
using ITHSystems.Views.Home;
using ITHSystems.Views.PickupService;

namespace ITHSystems.UnitTests.src.PickupServiceTest;

public class PickupService
{
    [Fact]
    public void PickupServiceTest()
    {
        var buildPickupModules = BuildHomeModules.GetPickupModules();
        var pickupServiceViewModel = new PickupServiceViewModel();
        Assert.NotNull(buildPickupModules);
        Assert.NotNull(pickupServiceViewModel);
        Assert.IsAssignableFrom<List<ModuleDto>>(buildPickupModules);
        Assert.IsType<PickupServiceViewModel>(pickupServiceViewModel);

    }
}
