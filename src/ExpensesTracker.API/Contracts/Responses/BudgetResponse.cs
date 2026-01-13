using ExpensesTracker.API.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Responses
{
    public class BudgetResponse
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category Category { get; set; }
    }
}
