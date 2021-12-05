using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserLoginDto
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }

    }
}
