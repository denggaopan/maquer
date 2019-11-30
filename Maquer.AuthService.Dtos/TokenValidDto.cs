using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Maquer.AuthService.Dtos
{
    public class TokenValidDto
    {
        [Required(ErrorMessage = "token不能为空")]
        public string Token { get; set; }
    }
}
