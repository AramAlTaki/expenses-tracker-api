using ExpensesTracker.API.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Responses
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsIncome { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Budget Budget { get; set; }
        public ICollection<TransactionResponse> Transactions { get; set; }
    }
}
