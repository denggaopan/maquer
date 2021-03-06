﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maquer.Common.Api;
using Maquer.Domain.User.Entities;
using Maquer.Repositories;
using Maquer.UserService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maquer.UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork uow,IMapper mapper) : base(uow)
        {
            _userRepo = _uow.Repository<User>();
            _mapper = mapper;
        }

        [HttpGet("all")]
        public ApiResult<List<UserDto>> Get()
        {
            var list = _userRepo.GetAll(a=> !a.IsDeleted);
            if(list == null || list.Count() == 0)
            {
                return new ApiResult<List<UserDto>> { Code = (int)ApiStatusCode.Fail, Message = "无数据" };
            }

            var dto = _mapper.Map<List<UserDto>>(list);
            return new ApiResult<List<UserDto>> { Code = (int)ApiStatusCode.Success, Data = dto };

        }

        [HttpGet("{id}")]
        public ApiResult<UserDto> Get(string id)
        {
            var entity = _userRepo.Find(id);
            if(entity == null)
            {
                return new ApiResult<UserDto> { Code = (int)ApiStatusCode.Fail, Message = "无数据" };
            }

            var userDto = _mapper.Map<UserDto>(entity);
            return new ApiResult<UserDto> { Code = (int)ApiStatusCode.Success, Data = userDto };
        }

        // POST api/values
        [HttpPost]
        public ApiResult<UserDto> Post([FromBody] UserCreationDto dto)
        {
            var isRegisted = _userRepo.Any(a=>a.UserName == dto.UserName);
            if (isRegisted)
            {
                return new ApiResult<UserDto> { Code = (int)ApiStatusCode.Fail,Message="用户名已存在"};
            }

            var entity = new User();
            entity.Id = Guid.NewGuid().ToString();
            entity.NickName = dto.NickName;
            entity.UserName = dto.UserName;
            entity.PasswordSalt = Guid.NewGuid().ToString("N");
            entity.Password = dto.Password;//后续需要加密处理
            entity.Email = dto.Email;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.CreatedTime = DateTime.Now;
            _userRepo.Add(entity);
            var success = _uow.SaveChanges();
            if (!success)
            {
                return new ApiResult<UserDto> { Code = (int)ApiStatusCode.Error, Message = "新增失败" };
            }

            var userDto = _mapper.Map<UserDto>(entity);
            return new ApiResult<UserDto> { Code = (int)ApiStatusCode.Success, Data = userDto };
        }


        [HttpDelete("{id}")]
        public ApiResult<object> Delete(string id)
        {
            _userRepo.Delete(id);
            var success = _uow.SaveChanges();
            if (!success)
            {
                return new ApiResult<object> { Code = (int)ApiStatusCode.Error, Message = "删除失败" };
            }
            return new ApiResult<object> { Code = (int)ApiStatusCode.Success, Message = "删除成功" };
        }
    }
}
