using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Requests
{
    public class CreateTransactionRequest
    {
        public Guid? CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string? CurrencyCode { get; set; }
        public bool IsIncome { get; set; }
        public DateOnly IssueDate { get; set; }
    }
}
