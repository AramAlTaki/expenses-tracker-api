using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Repositories;
using FluentValidation;

namespace ExpensesTracker.API.Validators
{
    public class UpdateBudgetValidator : AbstractValidator<UpdateBudgetRequest>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateBudgetValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category Id is required.")
                .MustAsync(BeValidCategory).WithMessage("Category does not exist.");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.")
                .LessThanOrEqualTo(1000000).WithMessage("Amount must be less or equal to 1,000,000.")
                .PrecisionScale(18, 4, false).WithMessage("Amount can have at most 4 decimal places.");

            RuleFor(x => x.Month)
                .NotEmpty().WithMessage("Month is required.")
                .InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("Year is required.")
                .InclusiveBetween(2000, 2100).WithMessage("Year must be between 2000 and 2100.");
        }

        private async Task<bool> BeValidCategory(Guid categoryId, CancellationToken cl)
        {
            if (categoryId == Guid.Empty)
                return false;

            var category = await _categoryRepository.GetByIdAsync(categoryId);

            return category != null;
        }
    }
}
