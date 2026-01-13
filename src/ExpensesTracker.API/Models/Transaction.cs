using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Data.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Transaction name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Range(1, 1000000,
        ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [StringLength(3, MinimumLength = 3,
        ErrorMessage = "Currency code must be a valid ISO 4217 code.")]
        public string? CurrencyCode { get; set; }

        public bool IsIncome { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        public DateTime CreatedAt { get; set; } 

        public Category Category { get; set; }
        public Image? Receipt { get; set; }
    }
}
