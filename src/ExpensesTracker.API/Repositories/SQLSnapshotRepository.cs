using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Data;
using ExpensesTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.API.Repositories
{
    public class SQLSnapshotRepository : ISnapshotRepository
    {
        private readonly ExpensesTrackerDbContext context;

        public SQLSnapshotRepository(ExpensesTrackerDbContext context)
        {
            this.context = context;
        }

        public async Task<Snapshot> CreateMonthlySnapshotAsync()
        {
            var now = DateTime.UtcNow;

            var lastSnapshot = await GetLatestSnapshotAsync();

            var startDate = lastSnapshot != null
                ? new DateTime(lastSnapshot.Year, lastSnapshot.Month, 1).AddMonths(1)
                : DateTime.MinValue;

            var transactions = await context.Transactions
                .Where(t => t.IssueDate >= startDate)
                .ToListAsync();

            decimal snapshotBalance = 
                (lastSnapshot?.Balance ?? 0 ) + transactions.Sum(t => t.IsIncome ? t.Amount : -t.Amount);

            var snapshot = new Snapshot
            {
                Id = Guid.NewGuid(),
                Balance = snapshotBalance,
                Month = now.Month,
                Year = now.Year,
            };

            await context.Snapshots.AddAsync(snapshot);
            await context.SaveChangesAsync();

            return snapshot;
        }

        public async Task<Snapshot?> GetLatestSnapshotAsync()
        {
            var snapshot = await context.Snapshots
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Month)
                .FirstOrDefaultAsync();

            if(snapshot == null)
            {
                return null;
            }

            return snapshot;
        }
    }
}
