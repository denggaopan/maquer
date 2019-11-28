using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Common.Database
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsDeleted{get;set;}
    }
}
