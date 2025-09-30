using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Attributes;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.CausesOfNonDelivery;
using ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverBeneficiary;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Web;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary;

[QueryProperty(nameof(PersonDto), "PersonDto")]
[RegisterViewModsel]
public partial class BeneficiaryViewModel : BaseViewModel
{
    #region
    [ObservableProperty]
    private PersonDto? personDto;
    [ObservableProperty]
    private ObservableCollection<DeliveryOptionDto?> deliveryOptions;
    [ObservableProperty]
    private DeliveryOptionDto? currentDeliveryOption;
    #endregion
    public BeneficiaryViewModel()
    {
        DeliveryOptions = UtilExtensions.GetDeliveryOptions()!;
    }

    #region
    [RelayCommand]
    public async Task Postpone()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            await PushRelativePageAsync<CausesOfNonDeliveryPage>();
        }
        catch (Exception e)
        {
            Debug.Write(e.Message);
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
        IsBusy = true;
        try
        {
            if(CurrentDeliveryOption is null)
            {
                await WarningAlert("Entregas", $"Debes seleccionar una opción de recepción para continuar la entrega");
                return;
            }

            switch (CurrentDeliveryOption.Id)
            {
                case Enums.Receiver.Beneficiary:
                    await PushRelativePageAsync<DeliverBenefiaryPage>();
                    break;
                case Enums.Receiver.SecondPerson:
                    await PushRelativePageAsync<DeliverSecondPersonPage>();
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.Write(e.Message);
            await ErrorAlert("Error", $"Error posponiendo entrega\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }
    [RelayCommand]
    private async Task SeeOnTheMap()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                await WarningAlert("Sin conexión", "No hay conexión a internet. Verifica tu conexión e intenta de nuevo.");
                return;
            }
            var action = await Shell.Current.DisplayActionSheet(
                "Ubicación", "Cancelar", null,
                "Ver en mapa", "Compartir ubicación");

            if (action == "Ver en mapa")
            {
                await OpenMapAsync();
            }
            else if (action == "Compartir ubicación")
            {
                await ShareLocationAsync();
            }
        }catch (Exception e)
        {
            Debug.Write(e.Message);
            await ErrorAlert("Error", $"Error mostrando ubicación\n{e.Message}");
        }
        finally { IsBusy = false; }
    }
    #endregion

    #region Methods
    private async Task OpenMapAsync()
    {
        if (PersonDto is null) return;

        if (PersonDto.FindByCorrdenates)
        {
            var location = new Location(PersonDto.Latitude, PersonDto.Longitude);
            await Map.OpenAsync(location, new MapLaunchOptions
            {
                Name = PersonDto.FullName ?? "Ubicación",
                NavigationMode = NavigationMode.Driving
            });
            return;
        }

        if (!string.IsNullOrWhiteSpace(PersonDto.Address))
        {
            var results = await Geocoding.GetLocationsAsync(PersonDto.Address);
            var loc = results?.FirstOrDefault();
            if (loc != null)
            {
                await Map.OpenAsync(new Location(loc.Latitude, loc.Longitude), new MapLaunchOptions
                {
                    Name = PersonDto.FullName ?? "Ubicación",
                    NavigationMode = NavigationMode.Driving
                });
            }
            else
            {
                await Shell.Current.DisplayAlert("Mapa", "No se pudo geolocalizar la dirección.", "OK");
            }
        }
    }

    private async Task ShareLocationAsync()
    {
        try
        {
            if (PersonDto is null) return;

            string title = $"Ubicación de {PersonDto.FullName ?? "Beneficiario"}";
            string uri;

            if (PersonDto.FindByCorrdenates)
            {
                // Google: https://maps.google.com/?q=lat,lng
                uri = $"https://maps.google.com/?q={PersonDto.Latitude},{PersonDto.Longitude}";
            }
            else if (!string.IsNullOrWhiteSpace(PersonDto.Address))
            {
                uri = $"https://maps.google.com/?q={HttpUtility.UrlEncode(PersonDto.Address)}";
            }
            else
            {
                await Shell.Current.DisplayAlert("Compartir", "No hay dirección ni coordenadas para compartir.", "OK");
                return;
            }

            await Share.RequestAsync(new ShareTextRequest
            {
                Title = title,
                Text = $"{title}\n{PersonDto.Address}",
                Uri = uri
            });
        }
        catch (Exception e)
        {
           Debug.Write(e.Message);
            await ErrorAlert("Error", $"Error compartiendo ubicación\n{e.Message}");
        }
       
    }
    #endregion
}
