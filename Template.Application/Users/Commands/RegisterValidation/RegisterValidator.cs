using FluentValidation;
using Template.Domain.Enums;

namespace Template.Application.Users.Commands.RegisterValidation
{
    public class RegisterValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Address)
              .NotEmpty()
              .When(x => x.Role == nameof(EnumRoleNames.User))
              .WithMessage("Address is required when registering a 'User'.");

            RuleFor(x => x.Address)
                .Empty()
                .When(x => x.Role != nameof(EnumRoleNames.User))
                .WithMessage("Address must be empty unless registering 'User'.");
            RuleFor(x => x.ClinicName)
               .NotEmpty()
               .When(x => x.Role == nameof(EnumRoleNames.User))
               .WithMessage("Clinic is required when registering a 'User'.");

            RuleFor(x => x.ClinicName)
                .Empty()
                .When(x => x.Role != nameof(EnumRoleNames.User))
                .WithMessage("Clinic must be empty unless registering a user is 'User'.");

            RuleFor(x => x.Longtitude)
                .NotEmpty()
                .Must(lon => lon != 0)
                .When(x => x.Role == nameof(EnumRoleNames.User))
                .WithMessage("Longtitude should not be 0 or empty when registering a 'User'.");

            RuleFor(x => x.Latitude)
                .NotEmpty()
                .Must(lat => lat != 0)
                .When(x => x.Role == nameof(EnumRoleNames.User))
                .WithMessage("Latitude should not be 0 or empty when registering a 'User'.");


            RuleFor(x => x.Longtitude)
                .NotEmpty()
                .Must(lon => lon == 0)
                .When(x => x.Role != nameof(EnumRoleNames.User))
                .WithMessage("Longtitude should be 0 unless registering a 'User'.");


            RuleFor(x => x.Latitude)
                .NotEmpty()
                .Must(lat => lat == 0)
                .When(x => x.Role != nameof(EnumRoleNames.User))
                .WithMessage("Latitude should be 0 unless registering a 'User'.");

        }
    }
}

