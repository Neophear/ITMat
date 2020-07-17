using ITMat.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Data.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetLoansAsync();
        Task<IEnumerable<Loan>> GetActiveLoansAsync();
        Task<IEnumerable<Loan>> GetFinishedLoansAsync();
        Task<Loan> GetLoanAsync(int id);
        Task<IEnumerable<LoanLineItem>> GetLoanLineItemsAsync(int loanId);
        Task<IEnumerable<LoanLineGenericItem>> GetLoanLineGenericItemsAsync(int loanId);
        Task<int> InsertLoanAsync(Loan loan);
        Task UpdateLoanAsync(int id, Loan loan);
        Task DeleteLoanAsync(int loanId);
    }
}