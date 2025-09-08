using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Controls;
using ITHSystems.Controls.ControlsPage.SignaturePad;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using System.Collections.ObjectModel;
using System.Diagnostics;

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

    public DeliverBeneficiaryViewModel()
    {
        Genders = UtilExtensions.GetGenders()!;
    }

    [RelayCommand]
    public async Task Delivery()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
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

            //            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            //            if (status != PermissionStatus.Granted)
            //            {
            //                await Permissions.RequestAsync<Permissions.Camera>();
            //            }



            //            if (MediaPicker.Default.IsCaptureSupported)
            //            {
            //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            //                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            //                if (photo != null)
            //                {
            //                    // save the file into local storage
            //                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            //                    using Stream sourceStream = await photo.OpenReadAsync();
            //                    using FileStream localFileStream = File.OpenWrite(localFilePath);

            //                    await sourceStream.CopyToAsync(localFileStream);
            //                }
            //            }

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();
                // await MediaPicker.CapturePhotoAsync();

                App.Current.MainPage = new CameraControlPage();
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"No se pudo tomar la foto.\n{ex.Message}", "OK");
        }
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
                        await PopAsync();
                    }

                }

            };


            await PushAsync(sigPage);

            await WarningAlert("Entrega", "Entrega firmada correctamente");

        }
        catch (Exception e)
        {
            Debug.Write(e.Message);
            await ErrorAlert("Entrega", "Error firmando la entrega.");
        }

    }


}
