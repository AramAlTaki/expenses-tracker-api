using ExpensesTracker.API.Data.Models;

namespace ExpensesTracker.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
        Task<Image> ReplaceImage(Guid id, Image image);
    }
}
