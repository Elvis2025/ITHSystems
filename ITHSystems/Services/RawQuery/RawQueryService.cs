using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Services.ApiManager;
using ITHSystems.Services.General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.Services.RawQuery
{
    [RegisterService]
    public class RawQueryService : IRawQueryService
    {
        private readonly IApiManagerService apiManagerService;

        public RawQueryService(IApiManagerService apiManagerService)
        {
            this.apiManagerService = apiManagerService;
        }

        public async Task<ResponseListDto<T>> ExcequteQueryList<T>(string query)
        {
            
            var content = new
            {
                jwt = IBS.Authentication.CurrentUser.JWT,
                sql = query,
            };

            var json = JsonConvert.SerializeObject(content);
            var request = IBS.HttpMethod.PostJson(IBS.RawQuery.Endpoint.Execute, json);

            var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);
            var userAuthentication = await IBS.HttpResponse.DeserealizeList<T>(response);
            return userAuthentication;
        }

        public async Task<T> ExcequteQueryObject<T>(string query)
        {
            
            var content = new
            {
                jwt = IBS.Authentication.CurrentUser.JWT,
                sql = query,
            };

            var json = JsonConvert.SerializeObject(content);
            var request = IBS.HttpMethod.PostJson(IBS.Authentication.Endpoint.CreateOrAuthToken, json);

            var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);
            var userAuthentication = await IBS.HttpResponse.DeserealizeObject<T>(response);
            return userAuthentication.Result;
        }


    }
}
