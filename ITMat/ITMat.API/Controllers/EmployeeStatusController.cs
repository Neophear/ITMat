using System.Collections.Generic;
using System.Threading.Tasks;
using ITMat.Core.Interfaces;
using ITMat.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ITMat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeStatusController : ControllerBase
    {
        private readonly IEmployeeStatusService service;

        public EmployeeStatusController(IEmployeeStatusService service)
            => this.service = service;

        // GET: api/<EmployeeStatusController>
        [HttpGet]
        public async Task<IEnumerable<EmployeeStatusDTO>> Get()
            => await service.GetEmployeeStatusesAsync();
    }
}