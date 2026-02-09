using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.API.Contracts.Requests
{
    public class UploadReceiptRequest
    {
        public Guid TransactionId { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
    }
}
