using ITMat.Core.Data.Interfaces;
using ITMat.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Data.Repositories
{
    public class CommentRepository : AbstractRepository<Comment>, ICommentRepository
    {
        #region Queries
        private const string SqlGetEmployeeComments = "select c.* from employee_comment ec inner join comment c on ec.comment_id = c.id where ec.employee_id = @employeeId",
                             SqlGetLoanComments = "select c.* from loan_comment lc inner join comment c on lc.comment_id = c.id where lc.loan_id = @loanId",
                             SqlGetComment = "select * from comment where id = @id",
                             SqlInsertEmployeeComment = "insert into comment ([username], [text]) values(@username, @text);" +
                                                            "declare @id int = scope_identity();" +
                                                            "insert into employee_comment (employee_id, comment_id) values(@employeeId, @id);" +
                                                            "select @id;",
                             SqlInsertLoanComment = "insert into comment ([username], [text]) values(@username, @text);" +
                                                            "declare @id int = scope_identity();" +
                                                            "insert into loan_comment (loan_id, comment_id) values(@loanId, @id);" +
                                                            "select @id;",
                             SqlUpdateComment = "update comment set [text] = @text where [Id] = @Id";
        #endregion

        public CommentRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task<IEnumerable<Comment>> GetEmployeeCommentsAsync(int employeeId)
            => await QueryMultipleAsync(SqlGetEmployeeComments, new { employeeId });

        public async Task<IEnumerable<Comment>> GetLoanCommentsAsync(int loanId)
            => await QueryMultipleAsync(SqlGetLoanComments, new { loanId });

        public async Task<Comment> GetCommentAsync(int id)
            => await QuerySingleAsync(SqlGetComment, new { id });

        public async Task<int> InsertEmployeeCommentAsync(int employeeId, Comment comment)
            => await QuerySingleAsync<int>(SqlInsertEmployeeComment, new { employeeId, comment.Username, comment.Text });

        public async Task<int> InsertLoanCommentAsync(int loanId, Comment comment)
            => await QuerySingleAsync<int>(SqlInsertLoanComment, new { loanId, comment.Username, comment.Text });

        public async Task UpdateCommentAsync(int id, string text)
        {
            var rowsAffected = await ExecuteAsync(SqlUpdateComment, new { id, text });

            if (rowsAffected != 1)
                throw new KeyNotFoundException($"Could not find comment with id {id}");
        }
    }
}