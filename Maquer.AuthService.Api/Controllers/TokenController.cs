using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Maquer.AuthService.Dtos;
using Maquer.Common.Api;
using Maquer.Domain.User.Entities;
using Maquer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Maquer.AuthService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : BaseController
    {
        private readonly IRepository<User> _userRepo;
        private readonly IConfiguration _configuration;
        public TokenController(IUnitOfWork uow,IConfiguration configuration):base(uow)
        {
            _configuration = configuration;
            _userRepo = _uow.Repository<User>();
           
        }
        [HttpPost]
        public async Task<ApiResult<string>> Post([FromBody] TokenQueryDto dto)
        {
            var entity = _userRepo.Get(a => a.UserName == dto.UserName);
            if(entity == null)
            {
                return new ApiResult<string> { Code = (int)ApiStatusCode.Fail, Message = "用户名或密码错误" };
            }
            var success = entity.Password == dto.Password;//密码加密后对比
            if(!success)
            {
                return new ApiResult<string> { Code = (int)ApiStatusCode.Fail, Message = "用户名或密码错误" };
            }

            var client = new HttpClient();
            DiscoveryResponse dr = await client.GetDiscoveryDocumentAsync(_configuration["IdentityServer:Url"]);
            if (dr.IsError)
            {
                return new ApiResult<string> { Code = (int)ApiStatusCode.Fail,Message= "认证服务器未启动" };
            }
            TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = dr.TokenEndpoint,
                ClientId = _configuration["IdentityServer:AuthServiceClient:ClientId"],
                ClientSecret = _configuration["IdentityServer:AuthServiceClient:ClientSecret"]
            });

            if (tokenResponse.IsError)
            {
                return new ApiResult<string> { Code = (int)ApiStatusCode.Fail, Message = tokenResponse.Error };
            }

            var login = new UserLogin() { 
                Token= tokenResponse.AccessToken,
                Expires=3600,
                User=entity

            };
            _uow.Repository<UserLogin>().Add(login);
            _uow.SaveChanges();

            return new ApiResult<string> { Code = (int)ApiStatusCode.Success,Message="SUCCESS", Data = tokenResponse.AccessToken };
        }

        [HttpPost("valid")]
        public ApiResult<bool> Valid([FromBody] TokenValidDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResult<bool> { Code = (int)ApiStatusCode.Error, Message = "数据校验不通过", Data = false };
            }
            var login = _uow.Repository<UserLogin>().Get(a=>a.Token == dto.Token);
            if(login == null)
            {
                return new ApiResult<bool> { Code = (int)ApiStatusCode.Fail, Message = "用户未曾登录",Data=false };
            }

            var success = login.CreatedTime.AddMinutes(login.Expires) >= DateTime.Now;
            if (!success)
            {
                return new ApiResult<bool> { Code = (int)ApiStatusCode.Fail, Message = "登录已过期", Data = false };
            }

            return new ApiResult<bool> { Code = (int)ApiStatusCode.Success, Message = "SUCCESS", Data = true };
        }


    }
}