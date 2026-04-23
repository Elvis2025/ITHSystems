using ITHSystems.Attributes;

namespace ITHSystems.Views.SyncData;
[RegisterAsRoute]
public partial class DownloadDataPage : BaseContentPage<DownloadDataViewModel>
{
	public DownloadDataPage(DownloadDataViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}