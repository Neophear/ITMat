using ITMat.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetEmployeeCommentsAsync(int employeeId);
        Task<IEnumerable<CommentDTO>> GetLoanCommentsAsync(int loanId);

        Task<int> CreateEmployeeCommentAsync(int employeeId, string text);
        Task<int> CreateLoanCommentAsync(int loanId, string text);

        Task DeleteCommentAsync(int id);
    }
}