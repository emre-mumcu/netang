using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.App_Data.DTO
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, StringLength(12, MinimumLength = 4)]
        public required string UserName { get; set; }

        [Required, StringLength(12, MinimumLength = 6)]
        public required string Password { get; set; }
    }
}