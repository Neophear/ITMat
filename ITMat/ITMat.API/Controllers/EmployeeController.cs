using ITMat.Core.Interfaces;
using ITMat.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static ITMat.API.Util.ApiTaskExtensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITMat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Domain Admins")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IEmployeeService employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            this.logger = logger;
            this.employeeService = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("{username} executed Get().", User.Identity.Name);
            return await TryOrError(logger, async () => await employeeService.GetEmployeesAsync());
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            logger.LogInformation("{username} executed Get({id}).", User.Identity.Name, id);
            return await TryOrError(logger, async () => await employeeService.GetEmployeeAsync(id));
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeDTO employee)
        {
            logger.LogInformation("{username} executed Post({employee}).", User.Identity.Name, employee?.Name);
            return await TryOrError(logger, async () =>
                {
                    var id = await employeeService.InsertEmployeeAsync(employee);
                    return new CreatedAtActionResult("Get", "Employee", new { id }, new { id });
                });
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeDTO employee)
        {
            logger.LogInformation("{username} executed Put({id}, {employee}).", User.Identity.Name, id, employee?.Name);
            return await TryOrError(logger, async () =>
                {
                    await employeeService.UpdateEmployeeAsync(id, employee);
                    return new NoContentResult();
                });
        }
    }
}