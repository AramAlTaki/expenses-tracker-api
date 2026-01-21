using ExpensesTracker.API.Contracts.Requests;
using FluentValidation;

namespace ExpensesTracker.API.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");

            RuleFor(x => x.IsIncome)
                .NotEmpty().WithMessage("IsIncome is required.");
        }
    }
}
