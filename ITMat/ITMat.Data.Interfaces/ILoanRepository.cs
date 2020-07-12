using System.Collections.Generic;
using System.Threading.Tasks;
using ITMat.Data.DTO;

namespace ITMat.Data.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<LoanDTO>> GetLoansAsync();
        Task<IEnumerable<LoanDTO>> GetActiveLoansAsync();
        Task<IEnumerable<LoanDTO>> GetFinishedLoansAsync();
        Task<LoanDTO> GetLoanAsync(int id);
        Task<int> InsertLoanAsync(LoanDTO loan);
        Task UpdateLoanAsync(int id, LoanDTO loan);
    }
}