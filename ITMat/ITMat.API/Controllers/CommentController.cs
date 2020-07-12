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
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ICommentService service;

        public CommentController(ILogger<CommentController> logger, ICommentService service)
        {
            this.logger = logger;
            this.service = service;
        }

        // GET: api/<CommentController>/employee/1
        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetEmployeeComments(int employeeId)
        {
            logger.LogInformation("{username} executed GetEmployeeComments({employeeId}).", User.Identity.Name, employeeId);
            return await TryOrError(logger, async () => await service.GetEmployeeCommentsAsync(employeeId));
        }

        // GET: api/<CommentController>/loan/1
        [HttpGet("loan/{loanId}")]
        public async Task<IActionResult> GetLoanComments(int loanId)
        {
            logger.LogInformation("{username} executed GetLoanComments({loanId}).", User.Identity.Name, loanId);
            return await TryOrError(logger, async () => await service.GetLoanCommentsAsync(loanId));
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            logger.LogInformation("{username} executed Get({id}).", User.Identity.Name, id);
            return await TryOrError(logger, async () => await service.GetCommentAsync(id));
        }

        // POST api/<CommentController>/employee/1
        [HttpPost("employee/{employeeId}")]
        public async Task<IActionResult> PostEmployeeComment(int employeeId, [FromBody] CommentDTO comment)
        {
            logger.LogInformation("{username} executed PostEmployeeComment({employeeId}, {comment}).", User.Identity.Name, employeeId, comment);
            return await TryOrError(logger, async () =>
                {
                    var id = await service.InsertEmployeeCommentAsync(employeeId, new CommentDTO { Username = User.Identity.Name, Text = comment.Text });
                    return new CreatedAtActionResult("Get", "Comment", new { id }, new { id });
                });
        }

        // POST api/<CommentController>/loan/1
        [HttpPost("loan/{employeeId}")]
        public async Task<IActionResult> PostLoanComment(int loanId, [FromBody] CommentDTO comment)
        {
            logger.LogInformation("{username} executed PostLoanComment({loanId}, {comment}).", User.Identity.Name, loanId, comment);
            return await TryOrError(logger, async () =>
                {
                    var id = await service.InsertLoanCommentAsync(loanId, new CommentDTO { Username = User.Identity.Name, Text = comment.Text });
                    return new CreatedAtActionResult("Get", "Comment", new { id }, new { id });
                });
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CommentDTO comment)
        {
            logger.LogInformation("{username} executed Put({id}, {comment}).", User.Identity.Name, id, comment);
            return await TryOrError(logger, async () =>
                {
                    await service.UpdateCommentAsync(id, comment);
                    return new NoContentResult();
                });
        }
    }
}