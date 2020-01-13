using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maquer.CatalogService.Dtos;
using Maquer.Common.Api;
using Maquer.Domain.Catalog.Entities;
using Maquer.Repositories;
using Maquer.UserService.ApiClient;
using Maquer.UserService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiClient;

namespace Maquer.CatalogService.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IRepository<Product> _productRepo;
        public ProductController(IUnitOfWork uow) : base(uow)
        {
            _productRepo = _uow.Repository<Product>();
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
        public ApiResult<IEnumerable<Product>> GetList(int pageNumber = 1, int pageSize = 10)
        {
            var q = _productRepo.GetAll(a => !a.IsDeleted);
            if (q == null || q.Count() == 0)
            {
                return new ApiResult<IEnumerable<Product>> { Code = (int)ApiStatusCode.Fail, Message = "无数据" };
            }

            var list = q.Skip((pageNumber - 1) * pageSize).Take(pageSize);
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
        public async Task<ApiResult<UserDto>> GetUserAsync([FromServices]IUserServiceApi userApi,string userId)
        {
            var ar =await userApi.GetAsync(userId);
            return ar;
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
