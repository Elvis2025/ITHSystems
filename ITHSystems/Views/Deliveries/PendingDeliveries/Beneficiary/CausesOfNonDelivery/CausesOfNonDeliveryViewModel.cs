using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.CausesOfNonDelivery;

public partial class CausesOfNonDeliveryViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<CausesOfNonDeliveryDto?> causeNonDeliveries;
    [ObservableProperty]
    private CausesOfNonDeliveryDto? currentCauseNonDeliveries;


    public CausesOfNonDeliveryViewModel()
    {
        CauseNonDeliveries = UtilExtensions.GetCausesOfNonDelivery()!;
    }

    [RelayCommand]
    public async Task SelectCauseOfNonDelivery()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            if (CurrentCauseNonDeliveries is null) return;
            await Shell.Current.DisplayAlert("Causa de No Entrega", $"Has seleccionado la causa: {CurrentCauseNonDeliveries.Name}", "OK");
        }
        catch (Exception e)
        {
            await ErrorAlert("Error", $"Error seleccionando causa de no entrega\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
