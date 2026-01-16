using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Requests
{
    public class GetTransactionsRequest
    {
        public Guid? CategoryId { get; set; }

        [Range(1, 12, ErrorMessage = "Start Month must be between 1 and 12.")]
        public int? StartMonth { get; set; }

        [Range(2000, 2100, ErrorMessage = "Start Year must be between 2000 and 2100.")]
        public int? StartYear { get; set; }

        [Range(1, 12, ErrorMessage = "End Month must be between 1 and 12.")]
        public int? EndMonth { get; set; }

        [Range(2000, 2100, ErrorMessage = "End Year must be between 2000 and 2100.")]
        public int? EndYear { get; set; }

        public bool? IsIncome { get; set; }

        [RegularExpression("^(Amount|IssueDate)$",
            ErrorMessage = "SortBy must be one of: Amount, IssueDate.")]
        public string? SortBy { get; set; } = "Amount";

        [RegularExpression("^(Asc|Desc)$",
            ErrorMessage = "SortOrder must be either 'Asc' or 'Desc'.")]
        public string? SortOrder { get; set; } = "Asc";

        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0.")]
        public int? Page { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int? PageSize { get; set; } = 10;
    }
}