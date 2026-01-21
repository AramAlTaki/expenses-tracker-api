using ExpensesTracker.API.Contracts.Requests;
using FluentValidation;

namespace ExpensesTracker.API.Validators
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionRequest>
    {
        public CreateTransactionValidator()
        {
            RuleFor(t => t.CategoryId)
                .NotEmpty()
                .When(t => t.CategoryId.HasValue);

            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Transaction name must not exceed 500 characters");

            RuleFor(t => t.Description)
                .MaximumLength(500).WithMessage("Transaction description must not exceed 500 characters.");

            RuleFor(t => t.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.")
                .LessThanOrEqualTo(1000000).WithMessage("Amount must be less or equal to 1,000,000.")
                .PrecisionScale(18, 4, false).WithMessage("Amount can have at most 4 decimal places.");

            RuleFor(t => t.CurrencyCode)
                .Length(3).WithMessage("Currency code must be a valid ISO 4217 code.");

            RuleFor(x => x.IsIncome)
                .NotEmpty().WithMessage("IsIncome is required.");

            RuleFor(t => t.IssueDate)
                .NotEmpty().WithMessage("Issue Date is required.")
                .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Issue date cannot be in the future.");
        }   
    }
}
