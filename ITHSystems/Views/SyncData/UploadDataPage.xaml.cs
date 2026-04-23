using ITHSystems.Attributes;

namespace ITHSystems.Views.SyncData;

[RegisterAsRoute]
public partial class UploadDataPage : BaseContentPage<UploadDataViewModel>
{
	public UploadDataPage(UploadDataViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}