using Azure.Core;
using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Models;
using ExpensesTracker.API.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImagesController : ApiControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IValidator<UploadReceiptRequest> _validator;

        public ImagesController(IImageRepository imageRepository, IValidator<UploadReceiptRequest> validator)
        {
            _imageRepository = imageRepository;
            _validator = validator;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadReceiptRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            var errorResponse = ValidationFailedResponse(validationResult);

            if (errorResponse != null)
                return errorResponse;

            var imageModel = new Image
            {
                TransactionId = request.TransactionId,
                File = request.File,
                Extension = Path.GetExtension(request.File.FileName),
                SizeInBytes = request.File.Length,
                FileName = request.File.FileName,
            };

            await _imageRepository.Upload(imageModel);

            return Ok(imageModel);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Upload(Guid id, [FromForm] UploadReceiptRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            var errorResponse = ValidationFailedResponse(validationResult);

            if (errorResponse != null)
                return errorResponse;

            var imageModel = new Image
            {
                TransactionId = request.TransactionId,
                File = request.File,
                Extension = Path.GetExtension(request.File.FileName),
                SizeInBytes = request.File.Length,
                FileName = request.File.FileName,
            };

            imageModel = await _imageRepository.Replace(id, imageModel);

            if(imageModel == null)
            {
                return NotFound();
            }

            return Ok(imageModel);
        }
    }
}
