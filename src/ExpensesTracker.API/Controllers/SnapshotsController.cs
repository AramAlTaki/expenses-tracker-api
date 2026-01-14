using AutoMapper;
using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Repositories;
using ExpensesTracker.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnapshotsController : ControllerBase
    {
        private readonly ISnapshotService snapshotService;
        private readonly IMapper mapper;

        public SnapshotsController(ISnapshotService snapshotService, IMapper mapper) 
        {
            this.snapshotService = snapshotService;
            this.mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSnapshot()
        {
            var snapshot = await snapshotService.CreateMonthlySnapshotAsync();
            return Ok(snapshot);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var snapshot = await snapshotService.GetLatestSnapshotAsync();

            if(snapshot == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SnapshotResponse>(snapshot));
        }
    }
}
