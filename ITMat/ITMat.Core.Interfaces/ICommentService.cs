using ITMat.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetEmployeeCommentsAsync(int employeeId);
        Task<IEnumerable<CommentDTO>> GetLoanCommentsAsync(int loanId);
        Task<CommentDTO> GetCommentAsync(int id);
        Task<int> InsertEmployeeCommentAsync(int employeeId, CommentDTO comment);
        Task<int> InsertLoanCommentAsync(int loanId, CommentDTO comment);
        Task UpdateCommentAsync(int id, string text);
    }
}