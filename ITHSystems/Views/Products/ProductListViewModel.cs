using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Attributes;
using ITHSystems.Extensions;
using ITHSystems.Repositories.SQLite;
using ITHSystems.Views.Sales.Dto;
using System.Collections.ObjectModel;

namespace ITHSystems.Views.Products;

[RegisterViewModsel]
public partial class ProductListViewModel : BaseViewModel
{
    private readonly IRepository<Model.Products> productsRepository;

    [ObservableProperty]
    private ObservableCollection<ProductDto> allProducts = new();

    [ObservableProperty]
    private int totalCount = 0;
    public ProductListViewModel(IRepository<Model.Products> productsRepository)
    {
        this.productsRepository = productsRepository;
        _ = LoadProductAsync();
    }

    [RelayCommand]
    public async Task Refresh()
    {
       await LoadProductAsync();
    }

    private async Task LoadProductAsync()
    {
        try
        {
            IsBusy = true;

            var products = await productsRepository.GetAllAsync();
            var productDto = products.Map<List<ProductDto>>();

            AllProducts = new ObservableCollection<ProductDto>(productDto.OrderByDescending(x => x.ImageId));
            TotalCount = productDto.Count;
        }
        catch
        {
        }
        finally
        {
            IsBusy = false;
        }
    }

}
