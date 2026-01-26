using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Requests
{
    public class CreateBudgetRequest
    {
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
