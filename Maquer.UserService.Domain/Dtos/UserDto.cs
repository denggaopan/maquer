using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.UserService.Domain.Dtos
{
    public class UserDto
    {
        public string Id  { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
