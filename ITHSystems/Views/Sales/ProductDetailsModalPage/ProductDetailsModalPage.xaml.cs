using ITHSystems.Attributes;

namespace ITHSystems.Views.Sales.ProductDetailsModalPage;

[RegisterAsRoute]

public partial class ProductDetailsModalPage : BaseContentPage<ProductDetailsViewModel>
{
	public ProductDetailsModalPage(ProductDetailsViewModel productDetailsViewModel) : base(productDetailsViewModel)
    {
		InitializeComponent();
	}
}

