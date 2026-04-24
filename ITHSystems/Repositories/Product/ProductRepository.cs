using ITHSystems.Attributes;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using ITHSystems.Model;
using ITHSystems.Repositories.SQLite;
using ITHSystems.Services.Produt;
using ITHSystems.Views.Sales.Dto;
using System.Threading.Channels;

namespace ITHSystems.Repositories.Product;


[RegisterRepository]
public class ProductRepository : IProductRepository
{
    private readonly IProductService productService;
    private readonly IRepository<Products> productRepository;

    public ProductRepository(IProductService productService,IRepository<Products> productRepository)
    {
        this.productService = productService;
        this.productRepository = productRepository;
    }

    public async Task DownloadProductsAsync(
        IProgress<ProgressBarDto> progress,
        CancellationToken cancellationToken = default)
    {

        await productRepository.DeleteAllAsync();

        var channel = Channel.CreateBounded<List<Products>>(new BoundedChannelOptions(2)
        {
            FullMode = BoundedChannelFullMode.Wait
        });

        int downloadedCount = 0;
        int insertedCount = 0;

        var producerTask = Task.Run(async () =>
        {
            try
            {
                int skip = 0;

                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = await productService.GetProductAsync(skip, 100);

                    var products = result?.Items?.ToList() ?? new List<Products>();

                    if (products.Count == 0)
                        break;

                    downloadedCount += products.Count;

                    progress.Report(new ProgressBarDto
                    {
                        DownloadedCount = downloadedCount,
                        InsertedCount = insertedCount,
                        Message = $"Descargados {downloadedCount} productos...",
                        Progress = 0
                    });

                    await channel.Writer.WriteAsync(products, cancellationToken);

                    skip += 100;

                    if (products.Count < 100)
                        break;
                }
            }
            finally
            {
                channel.Writer.Complete();
            }
        }, cancellationToken);

        var consumerTask = Task.Run(async () =>
        {
            await foreach (var products in channel.Reader.ReadAllAsync(cancellationToken))
            {
                await productRepository.InsertAllAsync(products);

                insertedCount += products.Count;

                progress.Report(new ProgressBarDto
                {
                    DownloadedCount = downloadedCount,
                    InsertedCount = insertedCount,
                    Message = $"Insertados {insertedCount} productos...",
                    Progress = downloadedCount == 0
                        ? 0
                        : (double)insertedCount / downloadedCount
                });
            }
        }, cancellationToken);

        await Task.WhenAll(producerTask, consumerTask);

        progress.Report(new ProgressBarDto
        {
            DownloadedCount = downloadedCount,
            InsertedCount = insertedCount,
            Message = "Productos sincronizados correctamente.",
            Progress = 1
        });
    }


    public async Task<List<ProductDto>> GetAllAsync()
    {
        try
        {
            var productsDto = await productRepository.GetAllAsync();

            return productsDto.OrderByDescending(x => x.ImageId).Map<List<ProductDto>>();
        }
        catch (Exception e)
        {
            throw new(e.Message);
        }
    }
}
