using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpensesTracker.API.Data.Models
{
    public class Category
    {
        [Required(ErrorMessage = "Id is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "IsIncome is required.")]
        public bool IsIncome { get; set; }
        public DateTime CreatedAt { get; set; }

        public Budget? Budget { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
