using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.API.Models
{
    public class Image
    {
        [Required(ErrorMessage = "Id is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Transaction Id is required.")]
        public Guid TransactionId { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "File is required.")]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "File Name is required.")]
        [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters.")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Extension is required.")]
        [RegularExpression(@"\.(jpg|jpeg|png|webp)$",
        ErrorMessage = "Only JPG, JPEG, PNG, or WEBP files are allowed.")]
        public string Extension { get; set; }

        [Required(ErrorMessage = "Size in bytes is required.")]
        [Range(1, 5 * 1024 * 1024,
        ErrorMessage = "Image size must be less than 5 MB.")]
        public long SizeInBytes { get; set; }

        [Required(ErrorMessage = "Path is required.")]
        [StringLength(500)]
        public string Path { get; set; }

        [Required(ErrorMessage = "CreatedAt is required.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Transaction is required.")]
        public Transaction Transaction { get; set; }
    }
}
