using ITMat.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Data.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetEmployeeCommentsAsync(int employeeId);
        Task<IEnumerable<Comment>> GetLoanCommentsAsync(int loanId);
        Task<Comment> GetCommentAsync(int id);
        Task<int> InsertEmployeeCommentAsync(int employeeId, Comment comment);
        Task<int> InsertLoanCommentAsync(int loanId, Comment comment);
        Task UpdateCommentAsync(int id, string text);
    }
}