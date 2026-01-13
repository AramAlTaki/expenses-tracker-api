using ExpensesTracker.API.Data;
using ExpensesTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.API.Repositories
{
    public class SQLTransactionRepository : ITransactionRepository
    {
        private readonly ExpensesTrackerDbContext context;

        public SQLTransactionRepository(ExpensesTrackerDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await context.Transactions.Include(t => t.Category).ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await context.Transactions.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction?> UpdateAsync(Guid id, Transaction transaction)
        {
            var transactionDomain = await context.Transactions.FirstOrDefaultAsync(t => t.Id == id);

            if(transactionDomain == null)
            {
                return null;
            }

            transactionDomain.Name = transaction.Name;
            transactionDomain.Description = transaction.Description;
            transactionDomain.Amount = transaction.Amount;
            transactionDomain.CurrencyCode = transaction.CurrencyCode;
            transactionDomain.IssueDate = transaction.IssueDate;

            await context.SaveChangesAsync();
            return transaction;
        }
    }
}
