using Microsoft.AspNetCore.Identity;
using Template.Domain.Entities.Notifications;
using Template.Domain.Entities.ProcedureRelatedEntities;
using Template.Domain.Entities.Schedule;
using Template.Domain.Entities.Users;

namespace Template.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? ProfileImagePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public List<Holiday> Holidays { get; set; } = [];

        public Clinic? Clinic { get; set; }
        public List<ProcedureAssistant>? InProcedure { get; set; } = [];
        public List<Procedure>? ProcedureFrom { get; set; } = [];
        public List<Device> Devices { get; set; } = [];

        // Ratings given by the doctor
        public List<Rating> RatingsGiven { get; set; } = [];

        // Ratings received by the assistant
        public List<Rating> RatingsReceived { get; set; } = [];
        public string? Otp { get; set; }
        public DateTime? ExpiryOtpDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? ForgotPasswordToken { get; set; }
    }
}
