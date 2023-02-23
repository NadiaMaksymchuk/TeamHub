using CollectionsAndLinq.Common.Models.Tasks;
using FluentValidation;

namespace CollectionsAndLinq.WebAPI.Validators
{
    public class TaskDtoValidator : AbstractValidator<NewTaskDto>
    {
        public TaskDtoValidator()
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

            RuleFor(u => u.PerformerId)
                .NotNull()
                .WithMessage("AuthorId is required")
                .GreaterThan(0)
                .WithMessage("AuthorId must be greater than 0");

            RuleFor(u => u.ProjectId)
                .NotNull()
                .WithMessage("AuthorId is required")
                .GreaterThan(0)
                .WithMessage("AuthorId must be greater than 0");
        }
    }
}
