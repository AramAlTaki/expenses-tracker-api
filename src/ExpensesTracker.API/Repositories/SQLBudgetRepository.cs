using ExpensesTracker.API.Data;
using ExpensesTracker.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.API.Repositories
{
    public class SQLBudgetRepository : IBudgetRepository
    {
        private readonly ExpensesTrackerDbContext dbContext;

        public SQLBudgetRepository(ExpensesTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Budget?> GetByIdAsync(Guid id)
        {
            return await dbContext.Budgets
                .Include("Category")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Budget> CreateAsync(Budget budget)
        {
            await dbContext.Budgets.AddAsync(budget);
            await dbContext.SaveChangesAsync();
            return budget;
        }

        public async Task<Budget> UpdateAsync(Guid id, Budget budget)
        {
            var existingBudget = await dbContext.Budgets.FindAsync(id);

            if (existingBudget == null)
            {
                return null;
            }

            existingBudget.CategoryId = budget.CategoryId;
            existingBudget.Amount = budget.Amount;
            existingBudget.Month = budget.Month;
            existingBudget.Year = budget.Year;

            await dbContext.SaveChangesAsync();
            return existingBudget;
        }

        public async Task<Budget> DeleteAsync(Guid id)
        {
            var existingBudget = await dbContext.Budgets.FindAsync(id);

            if (existingBudget == null)
            {
                return null;
            }

            dbContext.Budgets.Remove(existingBudget);
            dbContext.SaveChanges();
            return existingBudget;
        }
    }
}
