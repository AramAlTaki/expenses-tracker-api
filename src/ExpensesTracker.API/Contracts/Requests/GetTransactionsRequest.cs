using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Requests
{
    public class GetTransactionsRequest
    {
        [Range(1, 12, ErrorMessage = "StartMonth must be between 1 and 12.")]
        public int? StartMonth { get; set; }

        [Range(2000, 2100, ErrorMessage = "StartYear is out of valid range.")]
        public int? StartYear { get; set; }

        [Range(1, 12, ErrorMessage = "EndMonth must be between 1 and 12.")]
        public int? EndMonth { get; set; }

        [Range(2000, 2100, ErrorMessage = "EndYear is out of valid range.")]
        public int? EndYear { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "MinAmount must be greater than or equal to 0.")]
        public decimal? MinAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "MaxAmount must be greater than or equal to 0.")]
        public decimal? MaxAmount { get; set; }

        public Guid? CategoryId { get; set; }

        public bool? IsIncome { get; set; } = false;

        [RegularExpression("^(Amoun|IssueDate)$",
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