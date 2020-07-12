using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMat.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static ITMat.API.Util.ApiTaskExtensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITMat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ILoanService service;

        public LoanController(ILogger<LoanController> logger, ILoanService service)
        {
            this.logger = logger;
            this.service = service;
        }

        // GET: api/<LoanController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("");
            return await TryOrError(logger, async () => await service.GetLoansAsync());
        }

        // GET api/<LoanController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoanController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LoanController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoanController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
