using CollectionsAndLinq.Common.Models.Projects;
using FluentValidation;

namespace CollectionsAndLinq.WebAPI.Validators
{
    public class ProjectDtoValidator : AbstractValidator<NewProjectDto>
    {
        public ProjectDtoValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                    .WithMessage("Name is mandatory.")
                .MinimumLength(5)
                    .WithMessage("Name should be minimum 5 character.");

            RuleFor(u => u.Description)
                .NotEmpty()
                    .WithMessage("Description is mandatory.")
                .MinimumLength(10)
                    .WithMessage("Name should be minimum 10 character.");

            RuleFor(u => u.AuthorId)
                .NotNull()
                .WithMessage("AuthorId is required")
                .GreaterThan(0)
                .WithMessage("AuthorId must be greater than 0");

            RuleFor(u => u.Deadline)
                .NotNull()
                .WithMessage("Deadline is mandatory.")
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("Deadline can't be past");

            RuleFor(u => u.TeamId)
                .NotNull()
                .WithMessage("TeamId is required")
                .GreaterThan(0)
                .WithMessage("TeamId must be greater than 0");
        }
    }
}
