using ITMat.Core.DTO;
using ITMat.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
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
            logger.LogInformation("{username} executed Get().", User.Identity.Name);
            return await TryOrError(logger, async () => await service.GetLoansAsync());
        }

        // GET api/<LoanController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            logger.LogInformation("{username} executed Get({id}).", User.Identity.Name, id);
            return await TryOrError(logger, async () => await service.GetLoanAsync(id));
        }

        // POST api/<LoanController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateLoanDTO loan)
        {
            logger.LogInformation("{username} executed Post({loan}).", User.Identity.Name, loan);
            return await TryOrError(logger, async () =>
            {
                var id = await service.InsertLoanAsync(loan);
                return new CreatedAtActionResult("Get", "Loan", new { id }, new { id });
            });
        }

        // PUT api/<LoanController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LoanDTO loan)
        {
            logger.LogInformation("{username} executed Put({id}, {loan}).", User.Identity.Name, id, loan);
            return await TryOrError(logger, async () =>
            {
                await service.UpdateLoanAsync(id, loan);
                return new NoContentResult();
            });
        }

        [HttpPut("{loanId}/itemline/{id}")]
        public async Task<IActionResult> PutItemLine(int loanId, int id, [FromBody] UpdateLoanLineItemDTO line)
        {
            logger.LogInformation("{username} executed PutItemLine({loanId}, {id}, {line}).", User.Identity.Name, loanId, id, line);
            return await TryOrError(logger, async () =>
            {
                await service.UpdateLoanLineItemAsync(id, loanId, line.PickedUp, line.Returned);
                return new NoContentResult();
            });
        }

        [HttpPut("{loanId}/genericitemline/{id}")]
        public async Task<IActionResult> PutGenericItemLine(int loanId, int id, [FromBody] UpdateLoanLineGenericItemDTO line)
        {
            logger.LogInformation("{username} executed PutGenericItemLine({loanId}, {id}, {line}).", User.Identity.Name, loanId, id, line);
            return await TryOrError(logger, async () =>
            {
                await service.UpdateLoanLineGenericItemAsync(id, loanId, line.PickedUp, line.Returned);
                return new NoContentResult();
            });
        }

        // DELETE api/<LoanController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("{username} executed Delete({id}).", User.Identity.Name, id);
            return await TryOrError(logger, async () =>
            {
                await service.DeleteLoanAsync(id);
                return new NoContentResult();
            });
        }

        [HttpDelete("{loanId}/itemline/{id}")]
        public async Task<IActionResult> DeleteItemLine(int loanId, int id)
        {
            logger.LogInformation("{username} executed DeleteItemLine({loanId}, {id}).", User.Identity.Name, loanId, id);
            return await TryOrError(logger, async () =>
            {
                await service.DeleteLoanLineItemAsync(id, loanId);
                return new NoContentResult();
            });
        }
    }
}