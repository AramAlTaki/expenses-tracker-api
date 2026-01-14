using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Models
{
    public class Snapshot
    {
        [Required(ErrorMessage = "Id is required.")]
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "User Id is required.")]
        //public Guid UserId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, 1000000, ErrorMessage = "Amount must be between 1 and 1,000,000")]
        public decimal Balance { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
        public int Month { get; set; }

        [Required]
        [Range(2000, 2100, ErrorMessage = "Year must be between 2000 and 2100.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "CreatedAt is required.")]
        public DateTime CreatedAt { get; set; }
    }
}
