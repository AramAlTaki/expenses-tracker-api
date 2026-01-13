using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Responses
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsIncome { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
