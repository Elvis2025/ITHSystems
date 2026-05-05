using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Attributes;
using ITHSystems.DTOs;
using ITHSystems.Repositories.Product;
using ITHSystems.Repositories.SQLite;
using ITHSystems.Views.Products;

namespace ITHSystems.Views.SyncData;

[RegisterViewModsel]
public partial class DownloadDataViewModel : BaseViewModel
{
    private readonly IProductRepository productRepository;
    private readonly IRepository<Model.Products> productsRepository;
    private CancellationTokenSource? _cancellationTokenSource;
    [ObservableProperty]
    private bool isDownloading = false;
    [ObservableProperty]
    private double downloadProgress = 0;

    [ObservableProperty]
    private int downloadedProductsCount = 0;

    [ObservableProperty]
    private int insertedProductsCount = 0;
    [ObservableProperty]
    private int totalProduct = 0;

    [ObservableProperty]
    private string syncMessage = string.Empty;

    public DownloadDataViewModel(IProductRepository productRepository,
                                 IRepository<Model.Products> productsRepository
        )
    {
        this.productRepository = productRepository;
        this.productsRepository = productsRepository;
        InitialLoad();
    }

    [RelayCommand]
    private async Task DownloadProducts()
    {
        if (IsDownloading)
            return;

        if (await NoInternetConnection())
        {
            await WarningAlert(title: "iThot System", message: "No hay conexión a internet. Por favor, conéctese e intente nuevamente.");

            return;
        }
        try
        {
            IsDownloading = true;
            DownloadProgress = 0;
            DownloadedProductsCount = 0;
            InsertedProductsCount = 0;
            SyncMessage = "Iniciando descarga de productos...";

            _cancellationTokenSource = new CancellationTokenSource();

            var progress = new Progress<ProgressBarDto>(value =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DownloadedProductsCount = value.DownloadedCount;
                    InsertedProductsCount = value.InsertedCount;
                    DownloadProgress = value.Progress;
                    SyncMessage = value.Message;
                    InitialLoad();
                });
            });

            await productRepository.DownloadProductsAsync(
                progress,
                _cancellationTokenSource.Token);

        }
        catch (OperationCanceledException)
        {
            SyncMessage = "Descarga cancelada.";
        }
        catch (Exception ex)
        {
            SyncMessage = $"Error descargando productos: {ex.Message}";
        }
        finally
        {
            IsDownloading = false;
        }
    }

    [RelayCommand]
    public async Task ShowProducts()
    {
        try
        {
            if (IsBusy) return;

            IsBusy = true;
            await PushRelativePageAsync<ProductListPage>();
        }
        catch (Exception e)
        {
            await ErrorAlert("Error iThot System", $"Error al intentar navegar a la vista de productos: {e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void CancelDownload()
    {
        _cancellationTokenSource?.Cancel();
    }

    private void InitialLoad()
    {
        _ = MainThread.InvokeOnMainThreadAsync(async () =>
        {
            try
            {
                IsBusy = true;
                TotalProduct = await productsRepository.CountAsync();
            }
            catch (Exception e)
            {
                await ErrorAlert(title: "Error iThot System", message: e.Message);

            }
            finally
            {
                IsBusy = false;
            }
        });
    }
}
