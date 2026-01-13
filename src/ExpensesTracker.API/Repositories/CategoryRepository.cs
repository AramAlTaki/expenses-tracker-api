using ExpensesTracker.API.Data;
using ExpensesTracker.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.API.Repositories
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly ExpensesTrackerDbContext context;

        public SQLCategoryRepository(ExpensesTrackerDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid Id)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> UpdateAsync(Guid Id, Category category)
        {
            var categoryDomain = await context.Categories.FirstOrDefaultAsync(c => c.Id == Id);

            if(categoryDomain == null)
            {
                return null;
            }

            categoryDomain.Name = category.Name;
            categoryDomain.Description = category.Description;
            categoryDomain.IsIncome = category.IsIncome;

            await context.SaveChangesAsync();

            return categoryDomain;
        }

        public async Task<Category?> DeleteAsync(Guid Id)
        {
            var categoryDomain = context.Categories.FirstOrDefault(c => c.Id == Id);

            if (categoryDomain == null)
            {
                return null;
            }

            context.Categories.Remove(categoryDomain);
            await context.SaveChangesAsync();

            return categoryDomain;
        }
    }
}
