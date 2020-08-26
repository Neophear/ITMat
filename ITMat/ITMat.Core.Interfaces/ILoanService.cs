using ITMat.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanListedDTO>> GetLoansListedAsync();
        Task<IEnumerable<LoanDTO>> GetActiveLoansAsync();
        Task<IEnumerable<LoanDTO>> GetFinishedLoansAsync();
        Task<LoanDTO> GetLoanAsync(int id);
        Task<int> InsertLoanAsync(CreateLoanDTO dto);
        Task UpdateLoanAsync(int id, LoanDTO loan);
        Task UpdateLoanLineItemAsync(int id, int loanId, DateTime? pickedUp, DateTime? returned);
        Task UpdateLoanLineGenericItemAsync(int id, int loanId, DateTime? pickedUp, DateTime? returned);
        Task DeleteLoanAsync(int id);
        Task DeleteLoanLineItemAsync(int id, int loanId);
        Task DeleteLoanLineGenericItemAsync(int id, int loanId);
    }
}