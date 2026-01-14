using ExpensesTracker.API.Models;
using ExpensesTracker.API.Repositories;

namespace ExpensesTracker.API.Services
{
    public class SnapshotService : ISnapshotService
    {
        private readonly ISnapshotRepository snapshotRepository;

        public SnapshotService(ISnapshotRepository snapshotRepository)
        {
            this.snapshotRepository = snapshotRepository;
        }

        public async Task<Snapshot> CreateMonthlySnapshotAsync()
        {
            return await snapshotRepository.CreateMonthlySnapshotAsync();
        }

        public async Task<Snapshot?> GetLatestSnapshotAsync()
        {
            return await snapshotRepository.GetLatestSnapshotAsync();
        }
    }
}
