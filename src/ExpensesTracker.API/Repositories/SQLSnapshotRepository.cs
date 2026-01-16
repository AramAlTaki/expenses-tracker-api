using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Data;
using ExpensesTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.API.Repositories
{
    public class SQLSnapshotRepository : ISnapshotRepository
    {
        private readonly ExpensesTrackerDbContext context;
        private readonly ExpensesTrackerAuthDbContext authContext;

        public SQLSnapshotRepository(ExpensesTrackerDbContext context, ExpensesTrackerAuthDbContext authContext)
        {
            this.context = context;
            this.authContext = authContext;
        }

        public async Task<Snapshot> CreateMonthlySnapshotAsync(Guid userId)
        {
            var now = DateTime.UtcNow;

            var lastSnapshot = await GetLatestSnapshotAsync(userId);

            var startDate = lastSnapshot != null
                ? new DateTime(lastSnapshot.Year, lastSnapshot.Month, 1).AddMonths(1)
                : DateTime.MinValue;

            var transactions = await context.Transactions
                .Where(t => t.IssueDate >= startDate)
                .Where(t => t.UserId == userId)
                .ToListAsync();

            decimal snapshotBalance = 
                (lastSnapshot?.Balance ?? 0 ) + transactions.Sum(t => t.IsIncome ? t.Amount : -t.Amount);

            var snapshot = new Snapshot
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Balance = snapshotBalance,
                Month = now.Month,
                Year = now.Year,
            };

            await context.Snapshots.AddAsync(snapshot);
            await context.SaveChangesAsync();

            return snapshot;
        }

        public async Task<Snapshot?> GetLatestSnapshotAsync(Guid userId)
        {
            var snapshot = await context.Snapshots
                .Where(s => s.UserId == userId)
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
