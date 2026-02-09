using ExpensesTracker.API.Contracts.Requests;
using FluentValidation;

namespace ExpensesTracker.API.Validators
{
    public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionRequest>
    {
        public UpdateTransactionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Transaction name must not exceed 500 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Transaction description must not exceed 500 characters.");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.")
                .LessThanOrEqualTo(1000000).WithMessage("Amount must be less or equal to 1,000,000.")
                .PrecisionScale(18, 4, false).WithMessage("Amount can have at most 4 decimal places.");

            RuleFor(x => x.CurrencyCode)
                .Length(3).WithMessage("Currency code must be a valid ISO 4217 code.");

            RuleFor(x => x.IssueDate)
                .NotEmpty().WithMessage("Issue Date is required.")
                .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Issue date cannot be in the future.");
        }
    }
}
