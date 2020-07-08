using ITMat.Core.Interfaces;
using ITMat.Data.DTO;
using ITMat.Data.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ILogger logger;
        private readonly ICommentRepository repo;

        public CommentService(ILogger<CommentService> logger, ICommentRepository repo)
        {
            this.logger = logger;
            this.repo = repo;
        }

        public async Task<CommentDTO> GetCommentAsync(int id)
            => await repo.GetCommentAsync(id);

        public async Task<IEnumerable<CommentDTO>> GetEmployeeCommentsAsync(int employeeId)
            => await repo.GetEmployeeCommentsAsync(employeeId);

        public async Task<int> InsertEmployeeCommentAsync(int employeeId, CommentDTO comment)
            => await repo.InsertEmployeeCommentAsync(employeeId, new CommentDTO { Username = comment.Username, Text = comment.Text });

        public async Task UpdateCommentAsync(int id, CommentDTO comment)
            => await repo.UpdateCommentAsync(id, comment);
    }
}