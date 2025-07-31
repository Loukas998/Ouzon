using System.ComponentModel.DataAnnotations;
using Template.Application.Users.Dtos;

namespace Template.Application.Ratings.Dtos
{
    public class RatingsDto
    {
        public string? Note { get; set; }
        [Range(0, 5)]
        public int Rate { get; set; }
        public string? DoctorId { get; set; }
        public UserDto Doctor { get; set; }
        public string? AssistantId { get; set; }
        public UserDto Assistant { get; set; }
    }
}
