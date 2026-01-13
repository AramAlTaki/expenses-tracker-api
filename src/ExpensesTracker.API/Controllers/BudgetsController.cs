using AutoMapper;
using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Models;
using ExpensesTracker.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetRepository budgetRepository;
        private readonly IMapper mapper;

        public BudgetsController(IBudgetRepository budgetRepository, IMapper mapper)
        {
            this.budgetRepository = budgetRepository;
            this.mapper = mapper;
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
        public async Task<IActionResult> Create([FromBody] CreateBudgetRequest budgetRequest)
        {
            var budgetModel = mapper.Map<Budget>(budgetRequest);

            await budgetRepository.CreateAsync(budgetModel);

            var createdBudget = mapper.Map<BudgetResponse>(budgetModel);

            return CreatedAtAction(nameof(GetById), new { budgetModel.Id }, createdBudget);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBudgetRequest budgetRequest)
        {
            var budgetModel = mapper.Map<Budget>(budgetRequest);
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
