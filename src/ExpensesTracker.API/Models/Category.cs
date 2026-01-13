using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Data.Models
{
    public class Category
    {
        [Required(ErrorMessage = "Id is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Budget Id is required.")]
        public Guid BudgetId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public bool isIncome { get; set; }

        [Required(ErrorMessage = "CreatedAt is required.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Budget is required.")]
        public Budget Budget { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
