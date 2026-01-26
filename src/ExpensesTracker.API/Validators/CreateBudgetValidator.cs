using ExpensesTracker.API.Contracts.Requests;
using FluentValidation;

namespace ExpensesTracker.API.Validators
{
    public class CreateBudgetValidator : AbstractValidator<CreateBudgetRequest>
    {
        public CreateBudgetValidator()
        {
            RuleFor(b => b.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.")
                .Must(id => id != Guid.Empty).WithMessage("CategoryId cannot be an empty GUID.");

            RuleFor(b => b.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.")
                .LessThanOrEqualTo(1000000).WithMessage("Amount must be less or equal to 1,000,000.")
                .PrecisionScale(18, 4, false).WithMessage("Amount can have at most 4 decimal places.");

            RuleFor(b => b.Month)
                .NotEmpty().WithMessage("Month is required.")
                .InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");
            
            RuleFor(b => b.Year)
                .NotEmpty().WithMessage("Year is required.")
                .InclusiveBetween(2000, 2100).WithMessage("Year must be between 2000 and 2100.");

        }
    }
}
