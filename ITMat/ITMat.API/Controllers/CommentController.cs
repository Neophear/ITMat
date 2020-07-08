using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMat.Core.Interfaces;
using ITMat.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static ITMat.API.Util.ApiTaskExtensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITMat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("employee/{employeeid}")]
        public async Task<IActionResult> GetEmployeeComments(int employeeId)
            => await TryOrError(async () => await service.GetEmployeeCommentsAsync(employeeId));

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => await TryOrError(async () => await service.GetCommentAsync(id));

        // POST api/<CommentController>/employee/1
        [HttpPost("employee/{employeeId}")]
        public async Task<IActionResult> Post(int employeeId, [FromBody] CommentDTO comment)
            => await TryOrError(async () =>
            {
                var id = await service.InsertEmployeeCommentAsync(employeeId, new CommentDTO { Username = "UserName", Text = comment.Text });
                return new CreatedAtActionResult("Get", "Comment", new { id }, new { id });
            });

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
