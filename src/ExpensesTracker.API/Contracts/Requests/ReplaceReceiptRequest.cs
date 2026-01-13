using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Requests
{
    public class ReplaceReceiptRequest
    {
        [Required(ErrorMessage = "Transaction Id is required.")]
        public Guid TransactionId { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "File is required.")]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "File Name is required.")]
        [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters.")]
        public string FileName { get; set; }
    }
}
