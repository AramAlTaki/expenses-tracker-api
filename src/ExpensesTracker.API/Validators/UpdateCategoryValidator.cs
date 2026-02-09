using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Repositories;
using FluentValidation;

namespace ExpensesTracker.API.Validators
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Category description is required.")
                .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
        }
    }
}
