using ITHSystems.Attributes;

namespace ITHSystems.Views.Sales.ProductsCartList;

[RegisterAsRoute]
public partial class ProductsCartListPage : BaseContentPage<ProductsCartListPageViewModel>
{
	public ProductsCartListPage(ProductsCartListPageViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}