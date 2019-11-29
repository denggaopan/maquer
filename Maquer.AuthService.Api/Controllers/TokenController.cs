using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Maquer.AuthService.Dtos;
using Maquer.Common.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maquer.AuthService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        public async Task<ApiResult<string>> Post([FromBody] TokenQueryDto dto)
        {
            var client = new HttpClient();
            DiscoveryResponse dr = await client.GetDiscoveryDocumentAsync("http://localhost:5001");
            if (dr.IsError)
            {
                return new ApiResult<string> { Code = (int)ApiStatusCode.Success,Message= "认证服务器未启动" };
            }
            TokenResponse tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = dr.TokenEndpoint,
                ClientId = "AuthServiceClient",
                ClientSecret = "AuthServiceClient",
                UserName = dto.UserName,
                Password = dto.Password
            });

            if (tokenResponse.IsError)
            {
                return new ApiResult<string> { Code = (int)ApiStatusCode.Fail, Message = tokenResponse.Error };
            }

            return new ApiResult<string> { Code = (int)ApiStatusCode.Success,Message="SUCCESS", Data = tokenResponse.AccessToken };
        }


    }
}