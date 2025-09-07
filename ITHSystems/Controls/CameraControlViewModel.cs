using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.Controls
{
    public partial class CameraControlViewModel(ICameraProvider cameraProvider) : BaseViewModel
    {
        private readonly ICameraProvider cameraProvider = cameraProvider;

        //public IReadOnlyList<CameraInfo> Cameras => cameraProvider.AvailableCameras ?? [];

        //public static CancellationToken Token => CancellationToken.None;

        //public ICollection<CameraFlashMode> FlashModes { get; } = Enum.GetValues<CameraFlashMode>();

        //[ObservableProperty]
        //private CameraFlashMode flashMode;

        //[ObservableProperty]
        //private CameraInfo? selectedCamera;

        //[ObservableProperty]
        //private Size selectedResolution;

        //[ObservableProperty]
        //private float currentZoom;

        //[ObservableProperty]
        //private string? cameraNameText;

        //[ObservableProperty]
        //private string? zoomRangeText;

        //[ObservableProperty]
        //private string? currentZoomText;

        //[ObservableProperty]
        //private string? flashModeText;

        //[ObservableProperty]
        //private string? resolutionText;

        //[RelayCommand]
        //public async Task RefreshCameras(CancellationToken token) => await cameraProvider.RefreshAvailableCameras(token);

        //partial void OnFlashModeChanged(CameraFlashMode value)
        //{
        //    UpdateFlashModeText();
        //}

        //partial void OnCurrentZoomChanged(float value)
        //{
        //    UpdateCurrentZoomText();
        //}

        //partial void OnSelectedResolutionChanged(Size value)
        //{
        //    UpdateResolutionText();
        //}

        //void UpdateFlashModeText()
        //{
        //    if (SelectedCamera is null)
        //    {
        //        return;
        //    }
        //    FlashModeText = $"{(SelectedCamera.IsFlashSupported ? $"Flash mode: {FlashMode}" : "Flash not supported")}";
        //}

        //void UpdateCurrentZoomText()
        //{
        //    CurrentZoomText = $"Current Zoom: {CurrentZoom}";
        //}

        //void UpdateResolutionText()
        //{
        //    ResolutionText = $"Selected Resolution: {SelectedResolution.Width} x {SelectedResolution.Height}";
        //}
    }
}
