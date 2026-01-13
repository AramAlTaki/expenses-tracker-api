using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Requests
{
    public class UpdateCategoryRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "IsIncome is required.")]
        public bool IsIncome { get; set; }
    }
}
