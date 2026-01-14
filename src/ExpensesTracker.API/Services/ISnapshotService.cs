using ExpensesTracker.API.Models;

namespace ExpensesTracker.API.Services
{
    public interface ISnapshotService
    {
        Task<Snapshot> CreateMonthlySnapshotAsync();
        Task<Snapshot?> GetLatestSnapshotAsync();
    }
}
