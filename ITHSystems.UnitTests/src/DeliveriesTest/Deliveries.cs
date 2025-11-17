using ITHSystems.UnitTests.src.HomeTest;
using ITHSystems.Views.Deliveries;
using ITHSystems.Views.Home;

namespace ITHSystems.UnitTests.src.DeliveriesTest;

public class Deliveries
{
    [Fact]
    public void DeliveryTest()
    {
        var buildDeliveryModules = BuildHomeModules.GetDeliveriesModules();
        var deliveriesViewModel = new DeliveriesViewModel();

        Assert.NotNull(buildDeliveryModules);
        Assert.NotNull(deliveriesViewModel);
        Assert.IsType<DeliveriesViewModel>(deliveriesViewModel);
    }
}
