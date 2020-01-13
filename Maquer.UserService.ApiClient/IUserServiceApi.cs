using Maquer.Common.Api;
using Maquer.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using WebApiClient;
using WebApiClient.Attributes;

namespace Maquer.UserService.ApiClient
{
    [TraceFilter(OutputTarget = OutputTarget.Console | OutputTarget.Debug)]
    [HttpHost("http://localhost:5003/api/")] // HttpHost可以在Config传入覆盖
    public interface IUserServiceApi : IHttpApi
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("user/all")]
        [Timeout(10 * 1000)] // 10s超时
        ITask<IEnumerable<UserDto>> GetAllAsync();  


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpGet("user/{id}")]
        [Timeout(10 * 1000)] // 10s超时
        ITask<ApiResult<UserDto>> GetAsync([Required] string id);


        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="dto">用户信息</param>
        /// <returns></returns>
        [HttpGet("user")]
        [Timeout(10 * 1000)] // 10s超时
        ITask<UserDto> AddAsync([Required]UserCreationDto dto);
    }
}
