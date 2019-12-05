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
    public class Token2Controller : BaseController
    {
        private readonly IRepository<User> _userRepo;
        private readonly IConfiguration _configuration;
        public Token2Controller(IUnitOfWork uow,IConfiguration configuration):base(uow)
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
            var dr = await client.GetDiscoveryDocumentAsync(_configuration["IdentityServer:Url"]);
            if (dr.IsError)
            {
                return new ApiResult<string> { Code = (int)ApiStatusCode.Fail,Message= "认证服务器未启动" };
            }
            TokenResponse tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = dr.TokenEndpoint,
                ClientId = _configuration["IdentityServer:AuthServiceClient2:ClientId"],
                ClientSecret = _configuration["IdentityServer:AuthServiceClient2:ClientSecret"],
                UserName = _configuration["IdentityServer:AuthServiceClient2:UserName"],
                Password = _configuration["IdentityServer:AuthServiceClient2:Password"]
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


    }
}