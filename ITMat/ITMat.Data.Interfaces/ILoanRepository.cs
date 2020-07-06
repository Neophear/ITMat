using System.Collections.Generic;
using System.Threading.Tasks;
using ITMat.Data.DTO;

namespace ITMat.Data.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<LoanDTO>> GetLoansAsync();
    }
}