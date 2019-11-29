using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Maquer.UserService.Dtos
{
    public class UserCreationDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string NickName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
