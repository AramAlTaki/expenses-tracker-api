using AutoMapper;
using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using ExpensesTracker.API.Models;
using Microsoft.AspNetCore.Authorization;
namespace ExpensesTracker.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ApiControllerBase
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;

        public TransactionsController(ITransactionRepository transactionRepository, IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetTransactionsRequest request)
        {
            var transactionsDomain = await transactionRepository.GetAllAsync(request, GetUserId());

            return Ok(mapper.Map<List<TransactionResponse>>(transactionsDomain));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var transactionDomain = await transactionRepository.GetByIdAsync(id);

            if (transactionDomain is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TransactionResponse>(transactionDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionRequest request)
        {
            var transactionDomain = mapper.Map<Transaction>(request);

            transactionDomain.UserId = GetUserId();

            await transactionRepository.CreateAsync(transactionDomain);

            var transactionResponse = mapper.Map<TransactionResponse>(transactionDomain);

            return CreatedAtAction(nameof(GetById), new { id = transactionResponse.Id }, transactionResponse);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateTransactionRequest request)
        {
            var transactionDomain = mapper.Map<Transaction>(request);
            transactionDomain = await transactionRepository.UpdateAsync(id, transactionDomain);

            if (transactionDomain is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TransactionResponse>(transactionDomain));
        }
    }
}

