using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Models;

namespace ExpensesTracker.API.Repositories
{
    public interface ISnapshotRepository
    {
        Task<Snapshot> CreateMonthlySnapshotAsync();
        Task<Snapshot?> GetLatestSnapshotAsync(); 
    }
}
