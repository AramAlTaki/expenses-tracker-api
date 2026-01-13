using ExpensesTracker.API.Data;
using ExpensesTracker.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ExpensesTracker.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly ExpensesTrackerDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalImageRepository(ExpensesTrackerDbContext dbContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Image> Upload(Image image)
        {
            var uniqueFileName = $"{Guid.NewGuid()}{image.Extension}";
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{uniqueFileName}");
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{uniqueFileName}";
            image.Path = urlFilePath;
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }

        public async Task<Image> ReplaceImage(Guid id, Image newImage)
        {
            var existingImage = await dbContext.Images.FirstOrDefaultAsync(i => i.Id == id);
            if (existingImage == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(existingImage.Path))
            {
                var existingFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", Path.GetFileName(existingImage.Path));
                if (File.Exists(existingFilePath))
                {
                    File.Delete(existingFilePath);
                }
            }

            var uniqueFileName = $"{Guid.NewGuid()}{newImage.Extension}";
            var newFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", uniqueFileName);
            using var stream = new FileStream(newFilePath, FileMode.Create);
            await newImage.File.CopyToAsync(stream);

            var newUrlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{uniqueFileName}";

            existingImage.TransactionId = newImage.TransactionId;
            existingImage.Path = newUrlFilePath;
            existingImage.FileName = uniqueFileName;
            existingImage.Extension = newImage.Extension;
            existingImage.SizeInBytes = newImage.SizeInBytes;

            await dbContext.SaveChangesAsync();

            return existingImage;
        }


    }
}
