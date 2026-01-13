using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.API.Data.Models
{
    public class Image
    {
        public Guid Id { get; set; }

        [Required]
        public Guid TransactionId { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters.")]
        public string FileName { get; set; }

        [Required]
        [RegularExpression(@"\.(jpg|jpeg|png|webp)$",
        ErrorMessage = "Only JPG, JPEG, PNG, or WEBP files are allowed.")]
        public string Extension { get; set; }

        [Range(1, 5 * 1024 * 1024,
        ErrorMessage = "Image size must be less than 5 MB.")]
        public long SizeInBytes { get; set; }

        [Required]
        [StringLength(500)]
        public string Path { get; set; }

        public DateTime CreatedAt { get; set; }
        public Transaction Transaction { get; set; }
    }
}
