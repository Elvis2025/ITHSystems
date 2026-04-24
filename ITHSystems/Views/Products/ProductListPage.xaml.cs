using ITHSystems.Attributes;

namespace ITHSystems.Views.Products;

[RegisterAsRoute]
public partial class ProductListPage : BaseContentPage<ProductListViewModel>
{
	public ProductListPage(ProductListViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}