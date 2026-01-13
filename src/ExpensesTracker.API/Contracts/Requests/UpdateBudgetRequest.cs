using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Requests
{
    public class UpdateBudgetRequest
    {
        [Required(ErrorMessage = "Category Id is required.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, 1000000, ErrorMessage = "Amount must be between 1 and 1,000,000")]
        public decimal Amount { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
        public int Month { get; set; }

        [Required]
        [Range(2000, 2100, ErrorMessage = "Year must be between 2000 and 2100.")]
        public int Year { get; set; }
    }
}
