using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Model;
using ITHSystems.Services.RawQuery;
using ITHSystems.Views.Sales.Dto;

namespace ITHSystems.Services.Produt;

[RegisterService]
public class ProductService : IProductService
{
    private readonly IRawQueryService rawQueryService;

    public ProductService(IRawQueryService rawQueryService)
    {
        this.rawQueryService = rawQueryService;
    }

    public async Task<ResultDto<Products>> GetProductAsync(int skip, int take)
    {
        var tenantId = IBS.Authentication.CurrentLogin.Tenant.Id;

        var query = @$"SELECT 
                            Barcode,
                            p.Code,
                            Description, 
                            Tax, 
                            Cost,	
                            Price, 
                            [pi].Id AS ImageId, 
                            [pi].[Name] AS ImageName, 
                            [pi].Image AS Image,
                            [pi].Extension AS Extension,
                            p.ToSell
                        FROM Products p 
                        LEFT JOIN ProductImages [pi] ON p.Id = [pi].ProductId 
                        WHERE p.TenantId = {tenantId} 
                          AND p.ToSell = 1
                        ORDER BY p.Id
                        OFFSET {skip} ROWS
                        FETCH NEXT {take} ROWS ONLY";

        var result = await rawQueryService.ExcequteQueryList<Products>(query);

        return result?.Data ?? new ResultDto<Products>();
    }
}
