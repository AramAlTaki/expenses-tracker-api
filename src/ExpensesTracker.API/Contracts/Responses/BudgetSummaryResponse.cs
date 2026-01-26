namespace ExpensesTracker.API.Contracts.Responses
{
    public class BudgetSummaryResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal SpendingAmount { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}
