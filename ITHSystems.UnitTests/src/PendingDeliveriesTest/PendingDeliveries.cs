using ITHSystems.DTOs;
using ITHSystems.Extensions;
using ITHSystems.Services.General;
using ITHSystems.Views.Deliveries.PendingDeliveries;
using NSubstitute;

namespace ITHSystems.UnitTests.src.PendingDeliveriesTest;

public class PendingDeliveries
{
    [Fact]
    public void PendingDeliveriesTest()
    {
        var persons = UtilExtensions.GetPersons();
        var pendingDeliveriesViewModel = new PendingDeliveriesViewModel();

        Assert.NotNull(persons);
        Assert.IsAssignableFrom<IEnumerable<PersonDto>>(persons);
        Assert.IsType<PendingDeliveriesViewModel>(pendingDeliveriesViewModel);

        pendingDeliveriesViewModel.SearchPersonCommand.Execute(null);
        pendingDeliveriesViewModel.SelectPersonCommand.Execute(persons.First());
    }
}
