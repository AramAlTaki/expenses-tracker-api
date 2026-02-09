using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Requests
{
    public class GetTransactionsRequest
    {
        public Guid? CategoryId { get; set; }
        public int? StartMonth { get; set; }
        public int? StartYear { get; set; }
        public int? EndMonth { get; set; }
        public int? EndYear { get; set; }
        public bool? IsIncome { get; set; }
        public string? SortBy { get; set; } = "Amount";
        public string? SortOrder { get; set; } = "Asc";
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}