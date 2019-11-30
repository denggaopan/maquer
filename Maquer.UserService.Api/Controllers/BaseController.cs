using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maquer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maquer.UserService.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _uow;

        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }


    }
}