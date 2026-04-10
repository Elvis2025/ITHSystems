using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Attributes;
using ITHSystems.Services.Sale;
using ITHSystems.Views.Sales.Dto;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ITHSystems.Views.Sales;

[RegisterViewModsel]
public partial class SalesViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<ProductDto> products;

    [ObservableProperty]
    private ProductDto currentProduct = null!;
    [ObservableProperty]
    private string lastScannedBarcode = string.Empty;

    private List<ProductDto> allProducts = new();
    private readonly ISaleService saleService;
    [ObservableProperty]
    private string fiterText = string.Empty;

    [ObservableProperty]
    private bool openScannCodeBar = false;
    [ObservableProperty]
    private bool isScannerActive = false;

    [ObservableProperty]
    private int totalCount = 0;


    public SalesViewModel(ISaleService saleService)
    {
        this.saleService = saleService;
        Init();
    }

    public async Task GetProduct()
    {
        var result = await saleService.GetProductAsync();

        allProducts = result?.Items?.ToList() ?? new List<ProductDto>();
        Products = new ObservableCollection<ProductDto>(allProducts);
        TotalCount = Products.Count;
    }


    [RelayCommand]
    public async Task ScanBarcode()
    {
        if (OpenScannCodeBar)
        {
            OpenScannCodeBar = false;
            return;
        }

        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.Camera>();
        }

        if (status != PermissionStatus.Granted)
        {
            await Shell.Current.DisplayAlert("Permiso", "Se requiere acceso a la cámara", "OK");
            return;
        }

        OpenScannCodeBar = true;

        await Task.Delay(300); // 🔥 IMPORTANTE

        IsScannerActive = true; // bind a IsScanning




    }

    [RelayCommand]
    private async Task SearchProductByBarcode(string barcode)
    {
        if (string.IsNullOrWhiteSpace(barcode))
            return;

        LastScannedBarcode = barcode.Trim();

        var product = allProducts.FirstOrDefault(x =>
            (x.Barcode ?? string.Empty).Trim() == LastScannedBarcode);

        if (product is null)
        {


            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await WarningAlert("Producto no encontrado",
                    $"No se encontró un producto con el código {LastScannedBarcode}.");
            });
            return;
        }

        CurrentProduct = product;
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            CurrentProduct = product;
            await OpenProductDetails(product);
        });
        // Si quieres filtrar el listado:
        //   Products = new ObservableCollection<ProductDto>(new[] { CurrentProduct });

        // Aquí puedes también:
        // - abrir detalle
        // - agregar al carrito
        // - incrementar cantidad
    }
    [RelayCommand]
    public void FindProducts()
    {
        if (allProducts is null || allProducts.Count == 0)
        {
            Products = new ObservableCollection<ProductDto>();
            return;
        }

        var filter = FiterText?.Trim();

        if (string.IsNullOrWhiteSpace(filter))
        {
            Products = new ObservableCollection<ProductDto>(allProducts);
            return;
        }

        var filteredProducts = allProducts
            .Where(product =>
                (!string.IsNullOrWhiteSpace(product.Description) &&
                 product.Description.Contains(filter, StringComparison.OrdinalIgnoreCase))
                ||
                (!string.IsNullOrWhiteSpace(product.Barcode) &&
                 product.Barcode.Contains(filter, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        Products = new ObservableCollection<ProductDto>(filteredProducts);
        TotalCount = Products.Count;
    }

    [RelayCommand]
    public async Task OpenProductDetails(ProductDto? product)
    {
        try
        {
            if (IsBusy) return;
            IsBusy = true;

            if (product is null) return;

            CurrentProduct = product;

            await PushRelativePageAsync<ITHSystems.Views.Sales.ProductDetailsModalPage.ProductDetailsModalPage>(new Dictionary<string, object>
            {
                ["CurrentProduct"] = CurrentProduct
            });
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error opening product details: {e.Message}");
            await ErrorAlert("Error", $"Error opening product details\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
            CurrentProduct = null!;
        }
    }

    public async void Init()
    {

        _ = Task.Run(async () =>
        {
            try
            {
                IsBusy = true;
                await GetProduct();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                IsBusy = false;
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await ErrorAlert(title: "Error cargando productos", message: e.Message);
                });
            }
            finally
            {
                IsBusy = false;
            }
        });

    }
}
