using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Users.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = "User";
        public ClinicDto Clinic { get; set; }
        
    }
}
