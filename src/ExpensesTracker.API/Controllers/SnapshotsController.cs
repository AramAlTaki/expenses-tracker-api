using AutoMapper;
using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Repositories;
using ExpensesTracker.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SnapshotsController : ApiControllerBase
    {
        private readonly ISnapshotService snapshotService;
        private readonly ISnapshotRepository snapshotRepository;
        private readonly IMapper mapper;

        public SnapshotsController(ISnapshotService snapshotService, ISnapshotRepository snapshotRepository, IMapper mapper) 
        {
            this.snapshotService = snapshotService;
            this.snapshotRepository = snapshotRepository;
            this.mapper = mapper;
        }

        [HttpPost("create-test")]
        public async Task CreateSnapshotTest()
        {
            await snapshotService.RunMonthlySnapshots();
        }


        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var snapshot = await snapshotService.GetLatestSnapshotAsync(GetUserId());

            if(snapshot == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SnapshotResponse>(snapshot));
        }
    }
}
