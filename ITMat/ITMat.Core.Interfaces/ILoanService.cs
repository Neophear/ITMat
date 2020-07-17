using ITMat.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDTO>> GetLoansAsync();
        Task<IEnumerable<LoanDTO>> GetActiveLoansAsync();
        Task<IEnumerable<LoanDTO>> GetFinishedLoansAsync();
        Task<LoanDTO> GetLoanAsync(int id);
        Task<int> InsertLoanAsync(LoanDTO loan);
        Task UpdateLoanAsync(int id, LoanDTO loan);
    }
}