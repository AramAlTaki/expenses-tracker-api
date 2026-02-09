using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ExpensesTracker.API.Validators
{
    public class GetTransactionsValidator : AbstractValidator<GetTransactionsRequest>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetTransactionsValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(x => x.CategoryId)
                .MustAsync(BeValidCategory)
                .WithMessage("Category does not exist.");

            RuleFor(x => x.StartMonth)
                .InclusiveBetween(1, 12)
                .When(x => x.StartMonth.HasValue)
                .WithMessage("Start Month must be between 1 and 12.");

            RuleFor(x => x.EndMonth)
                .InclusiveBetween(1, 12)
                .When(x => x.EndMonth.HasValue)
                .WithMessage("End Month must be between 1 and 12.");

            RuleFor(x => x.StartYear)
                .InclusiveBetween(2000, 2100)
                .When(x => x.StartYear.HasValue)
                .WithMessage("Start Year must be between 2000 and 2100.");

            RuleFor(x => x.EndYear)
                .InclusiveBetween(2000, 2100)
                .When(x => x.EndYear.HasValue)
                .WithMessage("End Year must be between 2000 and 2100.");

            RuleFor(x => x)
                .MustAsync(BeBeforeEndDate)
                .WithMessage("Start Date must be before End Date.");

            RuleFor(x => x.SortBy)
                .Must(x => x == "Amount" || x == "IssueDate")
                .WithMessage("SortBy must be one of: Amount, IssueDate.");

            RuleFor(x => x.SortOrder)
                .Must(x => x == "Asc" || x == "Des")
                .WithMessage("SortOrder must be one of: Asc, Desc.");

            RuleFor(x => x.Page)
                .GreaterThan(0)
                .When(x => x.Page.HasValue)
                .WithMessage("Page must be greater than 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .When(x => x.PageSize.HasValue)
                .WithMessage("PageSize must be between 1 and 100.");
        }

        private async Task<bool> BeBeforeEndDate(GetTransactionsRequest request, CancellationToken cl)
        {
            var startMonth = request.StartMonth ?? 1;
            var endMonth = request.EndMonth ?? 1;

            var startYear = request.StartYear ?? 2000;
            var endYear = request.EndYear ?? 2100;

            var startDate = new DateTime(startYear, startMonth, 1);
            var endDate = new DateTime(endYear, endMonth, 1);

            return startDate <= endDate;
        }

        private async Task<bool> BeValidCategory(Guid? categoryId, CancellationToken cl)
        {
            if (!categoryId.HasValue)
                return true;

            if (categoryId.Value == Guid.Empty)
                return false;

            var category = await _categoryRepository.GetByIdAsync(categoryId.Value);

            return category != null;
        }
    }
}
