using Maquer.Common.Api;
using Maquer.Common.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Maquer.Common
{
    public class CustomAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomAuthMiddleware> _logger;

        public CustomAuthMiddleware(RequestDelegate next, ILogger<CustomAuthMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context,IConfiguration config)
        {
            var isValid = true;
            var method = context.Request.Method;
            var path = context.Request.Path.ToString();            
            StringValues auth;
            var res = context.Request.Headers.TryGetValue("Authorization",out auth);
            if (!res)
            {
                isValid = false;
                _logger.LogError($"no key 'Authorization' in headers");
            }
            else
            {
                var jwtBearer = auth[0];
                if (!jwtBearer.StartsWith("Bearer "))
                {
                    isValid = false;
                    _logger.LogError($"Authorization格式错误：Authorization：{jwtBearer}");
                }
                else
                {
                    var token = jwtBearer.Substring(7);

                    #region token状态
                    ////1.token状态
                    //try
                    //{
                    //    using (HttpClient client = new HttpClient())
                    //    {
                    //        var url = "http://localhsot:5002/api/token/valid";
                    //        var json = JsonConvert.SerializeObject(new { token });
                    //        var content = new StringContent(json);
                    //        var r = await client.PostAsync(url, content);
                    //        if (r.IsSuccessStatusCode)
                    //        {
                    //            var s = r.Content.ReadAsStringAsync().Result;
                    //            var ar = JsonConvert.DeserializeObject<ApiResult<bool>>(s);
                    //            if (ar.Code != 0 || !ar.Data)
                    //            {
                    //                isValid = false;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            isValid = false;
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    _logger.LogError($"token验证异常：ex：{ex.ToString()}");
                    //}
                    #endregion

                    //2.接口访问权限
                    var url = config["Gateway:Host"] + ServiceApiUrls.auth_token_validapiurl;
                    var apiUrl = path.Replace("api", config["Service:ShortName"].ToString().ToLower());
                    var data = new { token, apiUrl, method };
                    var r = await ServiceClient.PostAsync(url, data);
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        var ar = JsonConvert.DeserializeObject<ApiResult<bool>>(s);
                        if (ar.Code != 0 || !ar.Data)
                        {
                            isValid = false;
                        }
                    }
                    else
                    {
                        isValid = false;
                    }
                }

            }

            if (isValid)
            {
                await _next(context);
            }
            else
            {
                var result = new ApiResult<object> { Code = (int)ApiStatusCode.Fail, Message = "无权限" };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
