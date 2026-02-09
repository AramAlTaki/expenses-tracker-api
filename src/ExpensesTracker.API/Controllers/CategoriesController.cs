using AutoMapper;
using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Models;
using ExpensesTracker.API.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ApiControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateCategoryRequest> createValidator;
        private readonly IValidator<UpdateCategoryRequest> updateValidator;

        public CategoriesController(
            ICategoryRepository categoryRepository, 
            IMapper mapper,
            IValidator<CreateCategoryRequest> createValidator,
            IValidator<UpdateCategoryRequest> updateValidator) 
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.createValidator = createValidator;
            this.updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] int page = 1, [FromRoute] int pageSize = 15)
        {
            var categories = await categoryRepository.GetAllAsync(GetUserId(), page, pageSize);

            return Ok(mapper.Map<List<Category>>(categories));
        }

        [HttpGet]
        [Route("{Id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var category = await categoryRepository.GetByIdAsync(Id);

            if(category == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Category>(category));
        }

        [HttpGet]
        [Route("{Id:guid}/Budget-Information")]
        public async Task<IActionResult> GetInformation([FromRoute] Guid Id)
        {
            var category = await categoryRepository.GetByIdAsync(Id);

            var month = DateTime.UtcNow.Month;
            var budgetAmount = category.Budget.Amount;
            var spendingAmount = decimal.Zero;

            foreach (var transaction in category.Transactions)
            {
                if (transaction.IsIncome == false && transaction.IssueDate.Month == month)
                {
                    spendingAmount += transaction.Amount;
                }
            }
            var remainingAmount = budgetAmount - spendingAmount;

            var response = new BudgetSummaryResponse 
            {
                Name = category.Name,
                Description = category.Description,
                IssueDate = category.CreatedAt,
                BudgetAmount = budgetAmount,
                SpendingAmount = spendingAmount,
                RemainingAmount = remainingAmount
            };

            return Ok(response);        
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var validationResult = await createValidator.ValidateAsync(request);
            var errorResponse = ValidationFailedResponse(validationResult);

            if (errorResponse != null)
                return errorResponse;

            var category = mapper.Map<Category>(request);

            category.UserId = GetUserId();

            var createdCategory = await categoryRepository.CreateAsync(category);

            var categoryResponse = mapper.Map<Category>(createdCategory);

            return CreatedAtAction(nameof(GetById), new { createdCategory.Id }, categoryResponse);
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateCategoryRequest request)
        {
            var validationResult = await updateValidator.ValidateAsync(request);
            var errorResponse = ValidationFailedResponse(validationResult);

            if (errorResponse != null)
                return errorResponse;

            var category = mapper.Map<Category>(request);
            var updatedCategory = await categoryRepository.UpdateAsync(Id, category);

            if(updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Category>(updatedCategory));
        }

        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var deletedCategory = await categoryRepository.DeleteAsync(Id);

            if(deletedCategory == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Category>(deletedCategory));
        }
    }
}
