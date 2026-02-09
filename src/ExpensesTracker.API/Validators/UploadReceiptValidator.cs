using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Repositories;
using FluentValidation;

namespace ExpensesTracker.API.Validators
{
    public class UploadReceiptValidator : AbstractValidator<UploadReceiptRequest>
    {
        private const long MaxFileSize = 5 * 1024 * 1024;
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private readonly ITransactionRepository _transactionRepository;

        public UploadReceiptValidator(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;

            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("Transaction Id is required.")
                .MustAsync(BeValidTransaction).WithMessage("Transaction does not exist.");

            RuleFor(x => x.File)
                .NotNull().WithMessage("File is required.")
                .Must(file => file.Length > 0).WithMessage("File cannot be empty.")
                .Must(file => file.Length <= MaxFileSize).WithMessage("File cannot be larger than 5MB.")
                .Must(file => AllowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                    .WithMessage("Unsupported file extension.");

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("File Name is required.")
                .MaximumLength(255).WithMessage("File name cannot exceed 255 characters.");
        }

        private async Task<bool> BeValidTransaction(Guid transactionId, CancellationToken cl)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);

            return transaction != null;
        }
    }
}
