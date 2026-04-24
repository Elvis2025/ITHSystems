using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.Attributes;

namespace ITHSystems.Views.SyncData;
[RegisterViewModsel]
public partial class UploadDataViewModel : BaseViewModel
{
    [ObservableProperty]
    private bool isUploading = false;
    public UploadDataViewModel()
    {
        
    }
}
