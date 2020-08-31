using ITMat.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanEmployeeListedDTO>> GetLoansAsync();
        Task<IEnumerable<LoanListedDTO>> GetEmployeeLoansAsync(int employeeId);
        Task<LoanDTO> GetLoanAsync(int id);
    }
}