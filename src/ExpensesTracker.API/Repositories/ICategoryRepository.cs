using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Models;

namespace ExpensesTracker.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(Guid userId, int page, int pageSize);
        Task<Category?> GetByIdAsync(Guid Id);
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Guid Id, Category category);
        Task<Category?> DeleteAsync(Guid Id);
    }
}
