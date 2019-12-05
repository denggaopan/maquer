using AutoMapper;
using Maquer.Domain.User.Entities;
using Maquer.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maquer.UserService.Api
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserDto>();
        }
    }
}
