using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Contexts;

namespace Maquer.Common
{
    public class ApiTokenFilter : IApiActionFilter
    {
        private string _token { get; set; }

        public ApiTokenFilter(string token)
        {
            _token = token;
        }
        public Task OnBeginRequestAsync(ApiActionContext context)
        {            
            context.RequestMessage.Headers.Authorization =new AuthenticationHeaderValue("Bearer", _token);
            return Task.CompletedTask;
        }

        public Task OnEndRequestAsync(ApiActionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
