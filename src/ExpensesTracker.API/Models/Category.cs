namespace ExpensesTracker.API.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public Budget Budget { get; set; }

    }
}
