using CollectionsAndLinq.Common.Models.Teams;
using FluentValidation;

namespace ProjectStructure.BL.Validator
{
    public class TeamDtoValidator : AbstractValidator<NewTeamDto>
    {
        public TeamDtoValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                    .WithMessage("Name is mandatory.")
                .MinimumLength(5)
                    .WithMessage("Name should be minimum 5 character.");
        }
    }
}
