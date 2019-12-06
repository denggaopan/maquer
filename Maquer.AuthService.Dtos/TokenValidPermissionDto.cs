using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Maquer.AuthService.Dtos
{
    public class TokenValidPermissionDto
    {
        [Required(ErrorMessage = "token不能为空")]
        public string Token { get; set; }

        [Required(ErrorMessage = "apiurl不能为空")]
        public string ApiUrl { get; set; }

        [Required(ErrorMessage = "method不能为空")]
        public string Method { get; set; }
    }
}
