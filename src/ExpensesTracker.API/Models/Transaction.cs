namespace ExpensesTracker.API.Data.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string? CurrencyCode { get; set; }
        public bool IsIncome { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category Category { get; set; }
        public Image? Receipt { get; set; }
    }
}
