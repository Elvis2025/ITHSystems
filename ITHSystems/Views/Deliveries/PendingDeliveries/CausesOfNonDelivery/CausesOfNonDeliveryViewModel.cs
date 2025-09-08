
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Extensions;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.CausesOfNonDelivery;

public partial class CausesOfNonDeliveryViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<DTOs.CausesOfNonDelivery?> causesOfNonDeliverys;
    [ObservableProperty]
    private DTOs.CausesOfNonDelivery? currentCauseOfNonDeliverys;

    public CausesOfNonDeliveryViewModel()
    {
        CausesOfNonDeliverys = UtilExtensions.GetCausesOfNonDelivery()!;
    }

    [RelayCommand]
    public async Task SelectCauseOfNonDelivery()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            if (CurrentCauseOfNonDeliverys is null) return;
            
            await WarningAlert("Error", $"haz selccionado a {CurrentCauseOfNonDeliverys.Name}");
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
