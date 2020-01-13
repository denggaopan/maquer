using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maquer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maquer.CatalogService.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _uow;

        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        protected string Authorization
        {
            get
            {
                try
                {
                    string authorization = Request.Headers["Authorization"];
                    return authorization;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        protected string BearerToken
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Authorization) && Authorization.StartsWith("Bearer "))
                {
                    return Authorization.Substring(7);
                }
                return string.Empty;
            }
        }

    }
}