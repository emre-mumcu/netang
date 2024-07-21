using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.App_Data.DTO
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Token { get; set; }
    }
}