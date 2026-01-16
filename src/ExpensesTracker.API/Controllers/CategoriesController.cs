using ExpensesTracker.API.Repositories;
using ExpensesTracker.API.Models;
using ExpensesTracker.API.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ApiControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
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
