using ExpensesTracker.API.Contracts.Requests;
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

        public async Task<List<Transaction>> GetAllAsync(GetTransactionsRequest request, Guid userId)
        {
            var transactions = context.Transactions.Where(t => t.UserId == userId).Include(t => t.Category).Include(t => t.Receipt).AsQueryable();

            if(request.CategoryId != null)
            {
                transactions = transactions.Where(t => t.CategoryId == request.CategoryId);
            }

            if(request.IsIncome != null) 
            {
                transactions = request.IsIncome == true ? transactions.Where(t => t.IsIncome == true) : transactions.Where(t => t.IsIncome == false);
            }

            if(request.StartMonth != null && request.StartYear != null)
            {
                var startDate = new DateOnly((int) request.StartYear, (int) request.StartMonth, 1);
                transactions = transactions.Where(t => t.IssueDate >= startDate);
            }

            if(request.EndMonth != null && request.EndYear != null)
            {
                var endDate = new DateOnly((int)request.EndYear, (int)request.EndMonth, 1).AddMonths(1);
                transactions = transactions.Where(t => t.IssueDate <= endDate);
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Amount", StringComparison.OrdinalIgnoreCase))
                {
                    transactions = request.SortOrder.Equals("Asc") ? transactions.OrderBy(t => t.Amount) : transactions.OrderByDescending(t => t.Amount);
                }
                if (request.SortBy.Equals("IssueDate", StringComparison.OrdinalIgnoreCase))
                {
                    transactions = request.SortOrder.Equals("Asc") ? transactions.OrderBy(t => t.IssueDate) : transactions.OrderByDescending(t => t.IssueDate);
                }
            }

            var skipResults = (request.Page - 1) * request.PageSize;

            return await transactions.Skip((int) skipResults).Take((int) request.PageSize).ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await context.Transactions
                .Include(t => t.Category)
                .Include(t => t.Receipt)
                .FirstOrDefaultAsync(t => t.Id == id);
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

