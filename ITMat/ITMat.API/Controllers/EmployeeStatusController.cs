using ITMat.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static ITMat.API.Util.ApiTaskExtensions;

namespace ITMat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeStatusController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IEmployeeStatusService service;

        public EmployeeStatusController(ILogger<EmployeeStatusController> logger, IEmployeeStatusService service)
        {
            this.logger = logger;
            this.service = service;
        }

        // GET: api/<EmployeeStatusController>
        [HttpGet]
        public async Task<IActionResult> Get()
            => await TryOrError(logger, async () => await service.GetEmployeeStatusesAsync());
    }
}