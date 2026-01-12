using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Attributes;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using ITHSystems.Services.Delivery;
using ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ITHSystems.Views.Deliveries.PendingDeliveries;
[QueryProperty(nameof(Orders), "Orders")]
[RegisterViewModsel]
public partial class PendingDeliveriesViewModel : BaseViewModel
{
    #region
    [ObservableProperty]
    private ObservableCollection<PersonDto> persons = new();
    [ObservableProperty]
    private PersonDto? currentPerson;
    [ObservableProperty]
    private ObservableCollection<OrdersDto>? orders;

    [ObservableProperty]
    private string? fiterText;
    private readonly IDeliveryService deliveryService;
    #endregion
    public PendingDeliveriesViewModel(IDeliveryService deliveryService)
    {
        this.deliveryService = deliveryService;
        init();
    }

    public void init()
    {
        Task.Run(async () =>
        {
            IsBusy = true;
            if (Orders is null)
            {
                var _orders = await deliveryService.GetOrder();

                Orders = new(_orders.Data.Items);
            }
            Persons.Clear();
            foreach (var order in Orders!)
            {
                Persons.Add(
                    new PersonDto()
                    {
                        FirstName = order.Customer.FullName,
                        CardType = order.Description,
                        Address = order.DeliveryToAddress,
                        Code = order.Code,
                        Id = order.BatchProductId
                    });

            }
            IsBusy = false;
        });
    }
    #region Commands
    [RelayCommand]
    public async Task SelectPerson()
    {
        try
        {
            if (IsBusy) return;
            IsBusy = true;
            if (CurrentPerson is null) return;

            await PushRelativePageAsync<BeneficiaryPage>(new Dictionary<string, object>
            {
                ["PersonDto"] = CurrentPerson
            });

        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error selecting person: {e.Message}");
            await ErrorAlert("Error", $"Error selecting person\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
            CurrentPerson = null!;
        }
    }

    [RelayCommand]
    public async Task SearchPerson()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;

            if (string.IsNullOrEmpty(FiterText))
            {
                Persons = new(UtilExtensions.GetPersons().Where(x => x.Module == Enums.Modules.PENDINGDELIVERIES));
                return;
            }
            Persons = new(UtilExtensions.GetPersons()
                                        .Where(p => p.Module == Enums.Modules.PENDINGDELIVERIES &&
                                                    p.FullNameNormalize.Contains(FiterText.ToUpper()) ||
                                                    p.CardTypeNormalized.Contains(FiterText.ToUpper())));

        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error loading persons: {e.Message}");
            await ErrorAlert("Error", $"Error loading persons\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}
