using Azure.Core;
using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Models;
using ExpensesTracker.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadReceiptRequest request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageModel = new Image
                {
                    TransactionId = request.TransactionId,
                    File = request.File,
                    Extension = Path.GetExtension(request.File.FileName),
                    SizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                };

                await imageRepository.Upload(imageModel);

                return Ok(imageModel);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Upload(Guid id, [FromForm] ReplaceReceiptRequest request)
        {
            ValidateFileReplace(request);
            if (ModelState.IsValid)
            {
                var imageModel = new Image
                {
                    TransactionId = request.TransactionId,
                    File = request.File,
                    Extension = Path.GetExtension(request.File.FileName),
                    SizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                };

                imageModel = await imageRepository.Replace(id, imageModel);

                if(imageModel == null)
                {
                    return NotFound();
                }

                return Ok(imageModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(UploadReceiptRequest request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".webp" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (request.File.Length > 52425880)
            {
                ModelState.AddModelError("file", "File size more than 5MB, Please upload a smaller size file.");
            }
        }

        private void ValidateFileReplace(ReplaceReceiptRequest request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".webp" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (request.File.Length > 52425880)
            {
                ModelState.AddModelError("file", "File size more than 5MB, Please upload a smaller size file.");
            }
        }
    }
}
