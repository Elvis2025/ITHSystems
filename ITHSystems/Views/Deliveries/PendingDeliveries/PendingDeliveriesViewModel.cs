using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Deliveries.PendingDeliveries;

public partial class PendingDeliveriesViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<PersonDto> persons;
    public PendingDeliveriesViewModel()
    {
        Persons = new(UtilExtensions.GetPersons());
    }

}
