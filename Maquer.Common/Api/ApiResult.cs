using System;
using System.Collections.Generic;
using System.Text;


namespace Maquer.Common.Api
{
    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }
    }

    public class ApiResult
    {
        public int Code { get; set; } = 0;
        public string Message { get; set; }
    }
}
