using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Consul;
using Maquer.CatalogService.Dtos;
using Maquer.Common.Api;
using Maquer.Domain.Catalog.Entities;
using Maquer.Repositories;
using Maquer.UserService.ApiClient;
using Maquer.UserService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApiClient;

namespace Maquer.CatalogService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly IRepository<Product> _productRepo;
        private readonly IUserServiceApi _userApi;
        public ProductController(IConfiguration config, IUnitOfWork uow, IUserServiceApi userapi) : base(uow)
        {
            _config = config;
            _productRepo = _uow.Repository<Product>();
            _userApi = userapi;
        }

        [HttpGet("all")]
        public ApiResult<IEnumerable<Product>> GetAll()
        {
            var list = _productRepo.GetAll(a=>!a.IsDeleted);
            if(list == null || list.Count() == 0)
            {
                return new ApiResult<IEnumerable<Product>> { Code = (int)ApiStatusCode.Fail, Message = "无数据" };
            }
            return new ApiResult<IEnumerable<Product>> { Code = (int)ApiStatusCode.Success, Data = list };
        }

        [HttpGet("list")]
        public ApiResult<IEnumerable<Product>> GetList(int page = 1, int limit = 10)
        {
            var q = _productRepo.GetAll(a => !a.IsDeleted);
            if (q == null || q.Count() == 0)
            {
                return new ApiResult<IEnumerable<Product>> { Code = (int)ApiStatusCode.Fail, Message = "无数据" };
            }

            var list = q.Skip((page - 1) * limit).Take(limit);
            return new ApiResult<IEnumerable<Product>> { Code = (int)ApiStatusCode.Success, Data = list };
        }

        [HttpGet("{id}")]
        public ApiResult<Product> Get(string id)
        {
            var entity = _productRepo.Find(id);
            if (entity == null )
            {
                return new ApiResult<Product> { Code = (int)ApiStatusCode.Fail, Message = "无数据" };
            }
            return new ApiResult<Product> { Code = (int)ApiStatusCode.Success, Data = entity };
        }

        /// <summary>
        /// 测试微服务之间的调用
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user")]
        public async Task<ApiResult<object>> GetUserAsync(string userId)
        {
            var ar =await _userApi.GetAsync(userId).Retry(3);
            if(ar.Code == (int)ApiStatusCode.Success)
            {
                return new ApiResult<object> { Code = (int)ApiStatusCode.Success, Data = new { userId, data = ar.Data } };
            }
            return new ApiResult<object> { Code = ar.Code, Message=ar.Message, Data = new { userId, data = new object() } }; 
        }

        [HttpGet("user-list")]
        public async Task<ApiResult<object>> GetUserList()
        {
            
            using (var consulClient = new ConsulClient(a => a.Address = new Uri(_config["Consul:Host"])))
            {
                var services = consulClient.Catalog.Service("UserService").Result.Response;
                
                if (services != null && services.Any())
                {
                    Random r = new Random();
                    int index = r.Next(services.Count());
                    var service = services.ElementAt(index);

                    using (HttpClient client = new HttpClient())
                    {
                        var response = await client.GetAsync($"http://{service.ServiceAddress}:{service.ServicePort}/api/user/all");
                        var result = await response.Content.ReadAsStringAsync();
                        return new ApiResult<object>() { Code = 0, Data = result };
                    }
                }
            }
            return new ApiResult<object>() { Code = 1, Message="Error"};
        }

        // POST api/values
        [HttpPost]
        public ApiResult<Product> Post([FromBody] ProductCreationDto dto)
        {
            var entity = new Product();
            entity.Name = dto.Name;
            entity.SubName = dto.SubName;
            entity.Price = dto.Price;
            entity.IsActive = dto.IsActive;
            entity.CreatedTime = DateTime.Now;
            _productRepo.Add(entity);
            var success = _uow.SaveChanges();
            if (!success)
            {
                return new ApiResult<Product> { Code = (int)ApiStatusCode.Error, Message = "新增失败" };
            }

            return new ApiResult<Product> { Code = (int)ApiStatusCode.Success, Data = entity };
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
