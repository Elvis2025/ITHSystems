using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Controls;
using ITHSystems.Controls.ControlsPage.SignaturePad;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using System.Collections.ObjectModel;
using System.Diagnostics;
#if ANDROID
using Android.Content;
using Application = Android.App.Application;
#endif

#if IOS
using Foundation;
using UIKit;
using ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverSecondPerson;
#endif
namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverBeneficiary;

public partial class DeliverBeneficiaryViewModel : BaseViewModel
{
    [ObservableProperty]
    private bool isPhotoTaken = false;

    [ObservableProperty]
    private bool isSigned = false;
    [ObservableProperty]
    private string? personID;

    [ObservableProperty]
    private ImageSource? sign;

    [ObservableProperty]
    private ImageSource? photoId;

    [ObservableProperty]
    private ObservableCollection<GenderDto?> genders;
    [ObservableProperty]
    private GenderDto? currentGender;
    [ObservableProperty]
    private ObservableCollection<CountryDto?> countries;

    [ObservableProperty]
    private CountryDto? country;
    [ObservableProperty]
    private DateTime? expirationDate = null;

    public bool IsSecondPerson { get; set; } = false;

    public DeliverBeneficiaryViewModel()
    {
        Genders = UtilExtensions.GetGenders()!;
        Countries = UtilExtensions.GetCountries()!;
    }

    [RelayCommand]
    public async Task Delivery()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            if (IsSecondPerson)
            {
                if(Country is null)
                {
                    await WarningAlert("Entrega", "Debes especificar un pais antes de continuar.");
                    return;
                }
                if (ExpirationDate is null || ExpirationDate < DateTime.Now)
                {
                    await WarningAlert("Entrega", "Fecha de expiración incorrecta");
                    return;
                }
            }
            if (string.IsNullOrEmpty(PersonID))
            {
                await WarningAlert("Entrega", "La cedula del beneficiario es obligatoria.");
                return;
            }
            if (CurrentGender is null)
            {
                await WarningAlert("Entrega", "Debes especificar un genero antes de continuar.");
                return;
            }
            if (!IsPhotoTaken)
            {
                await WarningAlert("Entrega", "Debes tomar una foto de la cédula para continuar con el proceso de entgrega.");
                return;
            }
            if (!IsSigned)
            {
                await WarningAlert("Entrega", "La firma es obigatoria para continuar con el proceso de entrega.");
                return;
            }


            await Task.Delay(500);
            await SuccessAlert("Entrega", "Entrega de producto realizada correctamente");

            await Shell.Current.GoToAsync("..//..//..");

        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            await ErrorAlert("Delivery", "Error entregando producto.");
        }
        finally
        {
            IsSigned = false;
            IsPhotoTaken = false;
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task TakePhotoID()
    {
        try
        {
            var cameraPermissionStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (!cameraPermissionStatus.Equals(PermissionStatus.Granted))
            {
                var ok = await Shell.Current.DisplayAlert("Camara", $"Para ejecutar esta acción se require acceder a al permiso de la camara", "Dar Acceso", "Cancelar");
                if (ok)
                {
#if ANDROID
                    var context = Application.Context;
                    Intent intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
                    intent.AddFlags(ActivityFlags.NewTask);
                    var uri = Android.Net.Uri.FromParts("package", context.PackageName, null);
                    intent.SetData(uri);
                    context.StartActivity(intent);
#endif
#if IOS
                    var url = new NSUrl(UIApplication.OpenSettingsUrlString);
                    if (UIApplication.SharedApplication.CanOpenUrl(url))
                    {
                        UIApplication.SharedApplication.OpenUrl(url, new UIApplicationOpenUrlOptions(), null);
                    }
#endif
                    return;
                }
                return;
            }

            var cameraPage = new CameraControlPage()
            {
                OnSavePhoto = async (img, isSaved) =>
                {
                    if (img is not null && !img.IsEmpty && isSaved)
                    {
                        PhotoId = img;
                        IsPhotoTaken = true;
                    }
                    await PopAsync();
                }
            };
            await PushAsync(cameraPage);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"No se pudo tomar la foto.\n{ex.Message}", "OK");
        }
    }

    [RelayCommand]
    public async Task ScanCodeBarId()
    {
        var cameraPage = new CameraCodeBarControlPage();
        await PushAsync(cameraPage);
    }




    [RelayCommand]
    public async Task SignDelivery()
    {
        try
        {
            var sigPage = new SignaturePadPage()
            {
                OnSaveSignatrue = async (img, isSaved) =>
                {
                    if (img is not null && !img.IsEmpty && isSaved)
                    {
                        Sign = img;
                        IsSigned = true;
                    }
                    await PopAsync();

                }

            };

            await PushAsync(sigPage);
        }
        catch (Exception e)
        {
            Debug.Write(e.Message);
            await ErrorAlert("Entrega", "Error firmando la entrega.");
        }

    }

    [RelayCommand]
    public async Task ShowImageId()
    {
        if (PhotoId is null || PhotoId.IsEmpty) return;
        var imageView = new ImageViewControl(PhotoId)
        {

            CameraOn = async () =>
            {

                var cameraPage = new CameraControlPage(PhotoId)
                {
                    OnSavePhoto = async (img, isSaved) =>
                    {

                        if (img is not null && !img.IsEmpty && isSaved)
                        {
                            PhotoId = img;
                            IsPhotoTaken = true;
                        }
                        await PopAsync();
                    }
                };
                await PushAsync(cameraPage);
            }
        };
        await PushAsync(imageView);
    }
}
