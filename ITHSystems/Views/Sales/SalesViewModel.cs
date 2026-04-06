using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Attributes;
using ITHSystems.DTOs;
using ITHSystems.Services.RawQuery;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITHSystems.Views.Sales.ProductDetailsModalPage;

namespace ITHSystems.Views.Sales;

[RegisterViewModsel]
public partial class SalesViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<ProductDto> products;

    [ObservableProperty]
    private ProductDto currentProduct = null!;

    private List<ProductDto> allProducts = new();
    private readonly IRawQueryService rawQueryService;
    [ObservableProperty]
    private string fiterText = string.Empty;
    public SalesViewModel(IRawQueryService rawQueryService)
	{
        this.rawQueryService = rawQueryService;
        Init();
    }

    public async Task GetProduct()
    {
        var query = @$"SELECT 
                            p.Id AS Id, 
                            Barcode,	
                            Description, 
                            Tax, Cost,	
                            Price, 
                            [pi].Id AS ImageId, 
                            [pi].[Name] AS ImageName, 
                            [pi].Image AS Image,
                            [pi].Extension AS Extension 
                        FROM Products p 
                        INNER JOIN ProductImages [pi] ON p.Id = [pi].ProductId 
                        WHERE p.TenantId = 3";

        var result = await rawQueryService.ExcequteQueryList<ProductDto>(query);

        allProducts = result?.Data?.Items?.ToList() ?? new List<ProductDto>();
        Products = new ObservableCollection<ProductDto>(allProducts);
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
        
        _  = Task.Run(async () =>
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
                   await ErrorAlert(title: "Error cargando productos",message: e.Message);
                });
            }
            finally
            {
                IsBusy = false;
            }
        });
        
    }
}
