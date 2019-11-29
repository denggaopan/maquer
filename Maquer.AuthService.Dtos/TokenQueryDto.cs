using System;
using System.ComponentModel.DataAnnotations;

namespace Maquer.AuthService.Dtos
{
    public class TokenQueryDto
    {
        [Required(ErrorMessage = "用户名不可以为空")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不可以为空")]
        public string Password { get; set; }
    }
}
