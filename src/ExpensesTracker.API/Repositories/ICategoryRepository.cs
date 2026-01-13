using ExpensesTracker.API.Data.Models;

namespace ExpensesTracker.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid Id);
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Guid Id, Category category);
        Task<Category?> DeleteAsync(Guid Id);
    }
}
