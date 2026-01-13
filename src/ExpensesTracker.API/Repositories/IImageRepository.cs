using ExpensesTracker.API.Models;

namespace ExpensesTracker.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
        Task<Image?> Replace(Guid id, Image image);
    }
}
