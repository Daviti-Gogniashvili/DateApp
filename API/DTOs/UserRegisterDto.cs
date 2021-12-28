using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string userName { get; set; }

        [Required]
        [StringLength(64,MinimumLength = 8)]
        public string password { get; set; }
    }
}
