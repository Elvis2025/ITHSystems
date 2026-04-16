using ITHSystems.Attributes;

namespace ITHSystems.Views.Sales.ProductDetails;

[RegisterAsRoute]
public partial class ProductDetailsModalPage : BaseContentPage<ProductDetailsViewModel>
{
	public ProductDetailsModalPage(ProductDetailsViewModel productDetailsViewModel) : base(productDetailsViewModel)
    {
		InitializeComponent();
	}
}

