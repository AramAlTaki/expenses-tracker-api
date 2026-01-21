namespace ExpensesTracker.API.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsIncome { get; set; }
        public DateTime CreatedAt { get; set; }

        public Budget? Budget { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
