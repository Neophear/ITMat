using ITMat.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanListedDTO>> GetLoansAsync();
        Task<IEnumerable<LoanDTO>> GetEmployeeLoansAsync(int employeeId);
        Task<LoanDTO> GetLoanAsync(int id);
    }
}