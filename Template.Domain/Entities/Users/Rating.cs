using System.ComponentModel.DataAnnotations;

namespace Template.Domain.Entities.Users
{
    public class Rating : BaseEntity

    {
        public int Id { get; set; }
        public string? Note { get; set; }
        [Range(0, 5)]
        public int Rate { get; set; }
        public string? DoctorId { get; set; }
        public User Doctor { get; set; }
        public string? AssistantId { get; set; }
        public User Assistant { get; set; }
    }
}
