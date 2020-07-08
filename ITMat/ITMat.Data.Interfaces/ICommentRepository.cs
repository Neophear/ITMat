using ITMat.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Data.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentDTO>> GetEmployeeCommentsAsync(int employeeId);
        Task<CommentDTO> GetCommentAsync(int id);
        Task<int> InsertEmployeeCommentAsync(int employeeId, CommentDTO comment);
        Task UpdateCommentAsync(int id, CommentDTO comment);
    }
}