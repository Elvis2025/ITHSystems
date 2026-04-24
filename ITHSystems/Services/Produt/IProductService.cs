using ITHSystems.DTOs;
using ITHSystems.Model;

namespace ITHSystems.Services.Produt;

public interface IProductService
{
    Task<ResultDto<Products>> GetProductAsync(int skip, int take);
}
