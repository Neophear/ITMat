using ITMat.Core.Models;
using System;
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
        Task<IEnumerable<Loan>> GetEmployeeLoansAsync(int employeeId);
        Task<IEnumerable<LoanLineItem>> GetLoanLineItemsAsync(int loanId);
        Task<IEnumerable<LoanLineGenericItem>> GetLoanLineGenericItemsAsync(int loanId);
        Task<int> InsertLoanAsync(Loan loan, IEnumerable<int> itemIds, IEnumerable<int> genericItemIds);
        Task UpdateLoanAsync(int id, Loan loan);
        Task UpdateLoanLineItemAsync(int id, int loanId, DateTime? pickedUp, DateTime? returned);
        Task UpdateLoanLineGenericItemAsync(int id, int loanId, DateTime? pickedUp, DateTime? returned);
        Task DeleteLoanAsync(int loanId);
        Task DeleteLoanLineItemAsync(int id, int loanId);
        Task DeleteLoanLineGenericItemAsync(int id, int loanId);
    }
}