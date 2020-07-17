using ITMat.Core.Data.Interfaces;
using ITMat.Core.DTO;
using ITMat.Core.Interfaces;
using ITMat.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Services
{
    public class CommentService : AbstractService<Comment, CommentDTO>, ICommentService
    {
        private readonly ICommentRepository repo;

        public CommentService(ICommentRepository repo)
            => this.repo = repo;

        public async Task<CommentDTO> GetCommentAsync(int id)
            => Mapper.Map<CommentDTO>(await repo.GetCommentAsync(id));

        public async Task<IEnumerable<CommentDTO>> GetEmployeeCommentsAsync(int employeeId)
            => Mapper.Map<IEnumerable<CommentDTO>>(await repo.GetEmployeeCommentsAsync(employeeId));

        public async Task<IEnumerable<CommentDTO>> GetLoanCommentsAsync(int loanId)
            => Mapper.Map<IEnumerable<CommentDTO>>(await repo.GetEmployeeCommentsAsync(loanId));

        public async Task<int> InsertEmployeeCommentAsync(int employeeId, CommentDTO comment)
        {
            Validate(comment);
            return await repo.InsertEmployeeCommentAsync(employeeId, new Comment { Username = comment.Username, Text = comment.Text });
        }

        public async Task<int> InsertLoanCommentAsync(int loanId, CommentDTO comment)
        {
            Validate(comment);
            return await repo.InsertLoanCommentAsync(loanId, new Comment { Username = comment.Username, Text = comment.Text });
        }

        public async Task UpdateCommentAsync(int id, string text)
        {
            if (String.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            await repo.UpdateCommentAsync(id, text) ;
        }

        private void Validate(CommentDTO comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (String.IsNullOrEmpty(comment.Text))
                throw new ArgumentNullException(nameof(comment.Text));

            if (String.IsNullOrEmpty(comment.Username) || comment.Username.Length > 50)
                throw new ArgumentException($"{nameof(comment.Username)} can not be empty or longer than 50 characters.");
        }
    }
}