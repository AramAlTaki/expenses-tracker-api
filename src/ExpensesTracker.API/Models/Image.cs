using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.API.Models
{
    public class Image
    {
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        [RegularExpression(@"\.(jpg|jpeg|png|webp)$",
        ErrorMessage = "Only JPG, JPEG, PNG, or WEBP files are allowed.")]
        public string Extension { get; set; }

        [Range(1, 5 * 1024 * 1024,
        ErrorMessage = "Image size must be less than 5 MB.")]
        public long SizeInBytes { get; set; }

        public string Path { get; set; }

        public DateTime CreatedAt { get; set; }

        public Transaction Transaction { get; set; }
    }
}
