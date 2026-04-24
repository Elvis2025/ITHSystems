using ITHSystems.DTOs;
using ITHSystems.Views.Sales.Dto;

namespace ITHSystems.Repositories.Product;

public interface IProductRepository
{
    Task DownloadProductsAsync(IProgress<ProgressBarDto> progress, CancellationToken cancellationToken = default);
    Task<List<ProductDto>> GetAllAsync();
}
