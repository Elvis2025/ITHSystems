using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ITHSystems.Views.Deliveries.PendingDeliveries;

public partial class PendingDeliveriesViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<PersonDto> persons;
    [ObservableProperty]
    private PersonDto? currentPerson;


    [ObservableProperty]
    private string? fiterText;

    public PendingDeliveriesViewModel()
    {
        Persons = new(UtilExtensions.GetPersons());
    }

    [RelayCommand]
    public async Task SelectPerson()
    {
        try
        {   if (IsBusy) return;
            IsBusy = true;
            if (CurrentPerson is null) return;

            await PushRelativePageAsync<Beneficiary.Beneficiary>(new Dictionary<string, object>
            {
                ["PersonDto"] = CurrentPerson 
            });
         
        }catch (Exception e)
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
                await Task.Delay(300);
                Persons = new(UtilExtensions.GetPersons());
                return;
            }
            await Task.Delay(300);
            Persons = new(UtilExtensions.GetPersons().Where(p => p.FullNameNormalize.Contains(FiterText.ToUpper()) || p.CardTypeNormalized.Contains(FiterText.ToUpper())));
           
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

}
