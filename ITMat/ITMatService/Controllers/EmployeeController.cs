using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMat.Core.Interfaces;
using ITMat.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITMatService.Controllers
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
        public async Task<IEnumerable<EmployeeDTO>> Get()
        {
            logger.LogInformation(User.Identity.Name);
            return await employeeService.GetEmployeesAsync();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}