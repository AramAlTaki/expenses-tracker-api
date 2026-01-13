namespace ExpensesTracker.API.Contracts.Responses
{
    public class CategorySummaryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; }
    }
}
