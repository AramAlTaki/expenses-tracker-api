using ExpensesTracker.API.Models;

namespace ExpensesTracker.API.Services
{
    public interface ISnapshotService
    {
        Task RunMonthlySnapshots();
        Task<Snapshot?> GetLatestSnapshotAsync(Guid userId);
    }
}
