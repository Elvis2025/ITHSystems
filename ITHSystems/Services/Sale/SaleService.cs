using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Services.ApiManager;
using ITHSystems.Services.RawQuery;
using ITHSystems.Views.Sales.Dto;
using System.Collections.ObjectModel;

namespace ITHSystems.Services.Sale;
[RegisterService]
public class SaleService : ISaleService
{
    private readonly IRawQueryService rawQueryService;

    public SaleService(IRawQueryService rawQueryService)
    {
        this.rawQueryService = rawQueryService;
    }

    public async Task<ResultDto<ProductDto>> GetProductAsync()
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
                            [pi].Extension AS Extension,
                            p.ToSell
                        FROM Products p 
                        LEFT JOIN ProductImages [pi] ON p.Id = [pi].ProductId 
                        WHERE p.TenantId = {IBS.Authentication.CurrentLogin.Tenant.Id} AND p.ToSell = 1
                        ORDER BY ImageId DESC";

        var result = await rawQueryService.ExcequteQueryList<ProductDto>(query);

      var  allProducts = result?.Data ?? new DTOs.ResultDto<ProductDto>();

      return allProducts;
    }
}
