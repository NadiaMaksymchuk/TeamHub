using CollectionsAndLinq.Common.Models.Users;
using FluentValidation;

namespace ProjectStructure.BL.Validator
{
    public sealed class UserDtoValidator : AbstractValidator<NewUserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                    .WithMessage("FirstName is mandatory.")
                .MinimumLength(2)
                    .WithMessage("FirstName should be minimum 2 character.");

            RuleFor(u => u.LastName)
                .NotEmpty()
                    .WithMessage("FirstName is mandatory.")
                .MinimumLength(2)
                    .WithMessage("LastName should be minimum 2 character.");

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.BirthDay)
                .NotEmpty()
                .WithMessage("BirthDay is mandatory.")
                .LessThan(v => DateTime.Now);

            RuleFor(u => u.TeamId)
                .GreaterThan(0);
        }
    }
}
