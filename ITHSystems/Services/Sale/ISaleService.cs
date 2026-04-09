using ITHSystems.DTOs;
using ITHSystems.Views.Sales.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.Services.Sale;

public interface ISaleService
{
    Task<ResultDto<ProductDto>> GetProductAsync();
}
