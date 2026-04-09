using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Attributes;
using ITHSystems.Views.Sales.Dto;

namespace ITHSystems.Views.Sales.ProductDetailsModalPage;

[RegisterViewModsel]
[QueryProperty(nameof(CurrentProduct), "CurrentProduct")]
public partial class ProductDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    private ProductDto? currentProduct;

    [ObservableProperty]
    private int quantity = 1;

    public decimal Subtotal => (CurrentProduct?.Price ?? 0m) * Quantity;

    public decimal TaxAmount
    {
        get
        {
            var taxPercent = Convert.ToDecimal(CurrentProduct?.Tax ?? 0m);
            return Subtotal * (taxPercent / 100m);
        }
    }

    public decimal Total => Subtotal + TaxAmount;
    public ProductDetailsViewModel()
    {
            
    }
    partial void OnQuantityChanged(int value)
    {
        if (value < 1)
        {
            Quantity = 1;
            return;
        }

        NotifyTotalsChanged();
    }

    [RelayCommand]
    public void IncreaseQuantity()
    {
        Quantity++;
    }

    [RelayCommand]
    public void DecreaseQuantity()
    {
        if (Quantity > 1)
        {
            Quantity--;
        }
    }

   

    public void NotifyTotalsChanged()
    {
        OnPropertyChanged(nameof(Subtotal));
        OnPropertyChanged(nameof(TaxAmount));
        OnPropertyChanged(nameof(Total));
    }
}