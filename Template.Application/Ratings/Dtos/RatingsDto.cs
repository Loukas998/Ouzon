using System.ComponentModel.DataAnnotations;

namespace Template.Application.Ratings.Dtos
{
    public class RatingsDto
    {
        public int Id { get; set; }
        public string? Note { get; set; }
        [Range(0, 5)]
        public int Rate { get; set; }
        public string? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string? AssistantId { get; set; }
    }
}
