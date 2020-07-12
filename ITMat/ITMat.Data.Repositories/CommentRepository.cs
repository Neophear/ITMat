﻿using ITMat.Data.DTO;
using ITMat.Data.Interfaces;
using ITMat.Data.Repositories.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Data.Repositories
{
    internal class CommentRepository : AbstractDapperRepository<Comment, CommentDTO>, ICommentRepository
    {
        #region Queries
        private const string    SqlGetEmployeeComments = "select c.* from employee_comment ec inner join comment c on ec.comment_id = c.id where ec.employee_id = @employeeId",
                                SqlGetLoanComments = "select c.* from loan_comment lc inner join comment c on lc.comment_id = c.id where lc.loan_id = @loanId",
                                SqlGetComment = "select * from comment where id = @id",
                                SqlInsertEmployeeComment =  "insert into comment ([username], [text]) values(@username, @text);" +
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

        public async Task<IEnumerable<CommentDTO>> GetEmployeeCommentsAsync(int employeeId)
            => await QueryMultipleAsync(SqlGetEmployeeComments, new { employeeId });

        public async Task<IEnumerable<CommentDTO>> GetLoanCommentsAsync(int loanId)
            => await QueryMultipleAsync(SqlGetLoanComments, new { loanId });

        public async Task<CommentDTO> GetCommentAsync(int id)
            => await QuerySingleAsync(SqlGetComment, new { id });

        public async Task<int> InsertEmployeeCommentAsync(int employeeId, CommentDTO comment)
            => await QuerySingleAsync<int>(SqlInsertEmployeeComment, new { employeeId, comment.Username, comment.Text });

        public async Task<int> InsertLoanCommentAsync(int loanId, CommentDTO comment)
            => await QuerySingleAsync<int>(SqlInsertLoanComment, new { loanId, comment.Username, comment.Text });

        public async Task UpdateCommentAsync(int id, CommentDTO comment)
            => await QuerySingleAsync(SqlUpdateComment, new { id, comment.Text });
    }
}