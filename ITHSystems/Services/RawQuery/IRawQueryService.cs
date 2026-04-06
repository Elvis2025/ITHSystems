using ITHSystems.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.Services.RawQuery;

public interface IRawQueryService
{
    Task<ResponseListDto<T>> ExcequteQueryList<T>(string query);
    Task<T> ExcequteQueryObject<T>(string query);
}
