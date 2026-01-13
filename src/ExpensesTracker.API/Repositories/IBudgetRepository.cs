using ExpensesTracker.API.Data.Models;

namespace ExpensesTracker.API.Repositories
{
    public interface IBudgetRepository
    {
        Task<Budget?> GetByIdAsync(Guid id);
        Task<Budget> CreateAsync(Budget budget);
        Task<Budget?> UpdateAsync(Guid id, Budget budget);
        Task<Budget?> DeleteAsync(Guid id);
    }
}
