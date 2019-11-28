using System;
using System.Collections.Generic;
using System.Text;


namespace Maquer.Common.Api
{
    public class ApiResult<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
