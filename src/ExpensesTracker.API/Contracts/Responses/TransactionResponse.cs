namespace ExpensesTracker.API.Contracts.Responses
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string? CurrencyCode { get; set; }
        public bool IsIncome { get; set; }
        public DateOnly IssueDate { get; set; }
        public CategorySummaryResponse Category { get; set; }
        public ReceiptSummaryResponse Receipt { get; set; }
    }
}
