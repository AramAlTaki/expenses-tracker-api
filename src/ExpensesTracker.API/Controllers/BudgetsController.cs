using AutoMapper;
using Azure.Core;
using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Models;
using ExpensesTracker.API.Repositories;
using ExpensesTracker.API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BudgetsController : ApiControllerBase
    {
        private readonly IBudgetRepository budgetRepository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateBudgetRequest> createValidator;
        private readonly IValidator<UpdateBudgetRequest> updateValidator;

        public BudgetsController(
            IBudgetRepository budgetRepository, 
            IMapper mapper,
            IValidator<CreateBudgetRequest> createValidator, 
            IValidator<UpdateBudgetRequest> updateValidator)
        {
            this.budgetRepository = budgetRepository;
            this.mapper = mapper;
            this.createValidator = createValidator;
            this.updateValidator = updateValidator;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var budgetModel = await budgetRepository.GetByIdAsync(id);

            if (budgetModel == null) 
            {
                return NotFound();
            }

            return Ok(mapper.Map<BudgetResponse>(budgetModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBudgetRequest request)
        {
            var validationResult = await createValidator.ValidateAsync(request);
            var errorResponse = ValidationFailedResponse(validationResult);

            if (errorResponse != null)
                return errorResponse;

            var budgetModel = mapper.Map<Budget>(request);

            await budgetRepository.CreateAsync(budgetModel);

            var createdBudget = mapper.Map<BudgetResponse>(budgetModel);

            return CreatedAtAction(nameof(GetById), new { budgetModel.Id }, createdBudget);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBudgetRequest request)
        {
            var validationResult = await updateValidator.ValidateAsync(request);
            var errorResponse = ValidationFailedResponse(validationResult);

            if (errorResponse != null)
                return errorResponse;

            var budgetModel = mapper.Map<Budget>(request);
            budgetModel = await budgetRepository.UpdateAsync(id, budgetModel);

            if (budgetModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BudgetResponse>(budgetModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var budgetModel = await budgetRepository.DeleteAsync(id);

            if(budgetModel == null)
            { 
                return NotFound(); 
            }

            return Ok(mapper.Map<BudgetResponse>(budgetModel));
        }
    }
}
