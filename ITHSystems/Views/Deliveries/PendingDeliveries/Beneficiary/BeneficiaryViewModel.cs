using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.DTOs;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary;

[QueryProperty(nameof(PersonDto), "PersonDto")]
public partial class BeneficiaryViewModel : BaseViewModel
{
    [ObservableProperty]
    private PersonDto? personDto;

   
    public BeneficiaryViewModel()
    {
    }


    [RelayCommand]
    public async Task Postpone()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            await WarningAlert("Entregas", $"Hola {PersonDto?.FirstName} {PersonDto?.LastName} tu entrega ha sido pospuesta");
        }
        catch (Exception e)
        {
            await ErrorAlert("Error", $"Error posponiendo entrega\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task Deliver()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            await WarningAlert("Entregas", $"Hola {PersonDto?.FirstName} {PersonDto?.LastName} tu entrega ha sido Deliver");
        }
        catch (Exception e)
        {
            await ErrorAlert("Error", $"Error posponiendo entrega\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }



    [RelayCommand]
    public async Task SeeOnTheMap()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
        }
        catch (Exception e)
        {
            await ErrorAlert("Error", $"Error posponiendo entrega\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }



}
