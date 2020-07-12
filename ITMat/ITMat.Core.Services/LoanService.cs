using ITMat.Core.Interfaces;
using ITMat.Data.DTO;
using ITMat.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository repo;

        public LoanService(ILoanRepository repo)
            => this.repo = repo;

        public async Task<IEnumerable<LoanDTO>> GetActiveLoansAsync()
            => await repo.GetActiveLoansAsync();

        public async Task<IEnumerable<LoanDTO>> GetFinishedLoansAsync()
            => await repo.GetFinishedLoansAsync();

        public async Task<LoanDTO> GetLoanAsync(int id)
            => await repo.GetLoanAsync(id);

        public async Task<IEnumerable<LoanDTO>> GetLoansAsync()
            => await repo.GetLoansAsync();

        public async Task<int> InsertLoanAsync(LoanDTO loan)
            => await repo.InsertLoanAsync(loan);

        public async Task UpdateLoanAsync(int id, LoanDTO loan)
            => await repo.UpdateLoanAsync(id, loan);
    }
}