using ExpensesTracker.API.Data;
using ExpensesTracker.API.Models;
using ExpensesTracker.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.API.Services
{
    public class SnapshotService : ISnapshotService
    {
        private readonly ISnapshotRepository snapshotRepository;
        private readonly ExpensesTrackerAuthDbContext context;

        public SnapshotService(ISnapshotRepository snapshotRepository, ExpensesTrackerAuthDbContext context)
        {
            this.snapshotRepository = snapshotRepository;
            this.context = context;
        }

        public async Task RunMonthlySnapshots()
        {
            var users = await context.Users.Select(u => u.Id).ToListAsync();

            foreach (var userId in users)
            {
                await snapshotRepository.CreateMonthlySnapshotAsync(Guid.Parse(userId));
            }
        }

        public async Task<Snapshot?> GetLatestSnapshotAsync(Guid userId)
        {
            return await snapshotRepository.GetLatestSnapshotAsync(userId);
        }
    }
}
