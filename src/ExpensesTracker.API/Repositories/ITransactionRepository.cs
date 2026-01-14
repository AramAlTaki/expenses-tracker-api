using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Models;

namespace ExpensesTracker.API.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync(GetTransactionsRequest request);
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<Transaction> CreateAsync(Transaction transaction);
        Task<Transaction?> UpdateAsync(Guid id, Transaction transaction);
    }
}
