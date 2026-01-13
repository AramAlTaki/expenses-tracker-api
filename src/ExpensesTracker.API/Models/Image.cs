using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.API.Data.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public long SizeInBytes { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public Transaction Transaction { get; set; }
    }
}
