using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.CausesOfNonDelivery;
[RegisterViewModsel]
public partial class CausesOfNonDeliveryViewModel : BaseViewModel
{
    #region Properties
    [ObservableProperty]
    private ObservableCollection<CausesOfNonDeliveryDto>? causeNonDeliveries;
    [ObservableProperty]
    private CausesOfNonDeliveryDto? currentCauseNonDeliveries;
    #endregion 

    public CausesOfNonDeliveryViewModel()
    {
        Init();
    }

    #region Commands
    [RelayCommand]
    public async Task SelectCauseOfNonDelivery()
    {
        if (IsBusy) return;
        try
        {
            if (CurrentCauseNonDeliveries?.Id < 0) return;
            Preferences.Set(PreferencesKeys.CasesNonDeliveries, CurrentCauseNonDeliveries!.Id);
            await PushPopupAsync<NonDeliveryPopup>();
        }
        catch (Exception e)
        {
            await ErrorAlert("Error", $"Error seleccionando causa de no entrega\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
            CurrentCauseNonDeliveries = new();
        } 
    }

    [RelayCommand]
    public async Task ClosePopup()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            await PopPopupAsync();
        }
        catch (Exception e)
        {
            await ErrorAlert("Error", $"Error cerrando popup\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }
    [RelayCommand]
    public async Task SaveNonDelivery()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            await PopPopupAsync();
            await SuccessAlert("Éxito", $"Razón de no entrega guardada correctamente.");
            await Shell.Current.GoToAsync("..//..//..");
        }
        catch (Exception e)
        {
            await ErrorAlert("Error", $"Error cerrando popup\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion


    #region Methods
    public void Init()
    {
        CauseNonDeliveries = UtilExtensions.GetCausesOfNonDelivery()!;
        var NonDeliveriesId = Preferences.Get(PreferencesKeys.CasesNonDeliveries, -1);
        if (!CauseNonDeliveries.Any(x => x!.Id == NonDeliveriesId)) return;
        CurrentCauseNonDeliveries = CauseNonDeliveries.FirstOrDefault(c => c!.Id == NonDeliveriesId);
    }
    #endregion
}
