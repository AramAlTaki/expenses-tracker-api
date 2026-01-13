using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Data.Models
{
    public class Transaction
    {
        [Required(ErrorMessage = "Id is required.")]
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "User Id is required.")]
        //public Guid UserId { get; set; }

        [Required(ErrorMessage = "Category Id is required.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 1000000, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency code must be a valid ISO 4217 code.")]
        public string? CurrencyCode { get; set; }

        [Required(ErrorMessage = "isIncome is required.")]
        public bool IsIncome { get; set; }

        [Required(ErrorMessage = "Issue Date is required.")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "CreatedAt is required.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public Category Category { get; set; }
        public Image? Receipt { get; set; }
    }
}
