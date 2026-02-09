using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpensesTracker.API.Models
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

        [JsonIgnore]
        public Transaction Transaction { get; set; }
    }
}
