using ITHSystems.Attributes;

namespace ITHSystems.Views.Clients;
[RegisterAsRoute]
public partial class ClientList : BaseContentPage<ClientListViewModel>
{
	public ClientList(ClientListViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}