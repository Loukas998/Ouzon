using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Users.Dtos
{
   public class ClinicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public float Longtitude { get; set; }
        public float Latitude { get; set; }

    }
}
