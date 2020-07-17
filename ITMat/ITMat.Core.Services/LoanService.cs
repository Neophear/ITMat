using ITMat.Core.Data.Interfaces;
using ITMat.Core.DTO;
using ITMat.Core.Interfaces;
using ITMat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMat.Core.Services
{
    public class LoanService : AbstractService<Loan, LoanDTO>, ILoanService
    {
        private readonly ILoanRepository repo;

        public LoanService(ILoanRepository repo)
            => this.repo = repo;

        public async Task<IEnumerable<LoanDTO>> GetActiveLoansAsync()
            => Mapper.Map<IEnumerable<LoanDTO>>(await repo.GetActiveLoansAsync());

        public async Task<IEnumerable<LoanDTO>> GetFinishedLoansAsync()
            => Mapper.Map<IEnumerable<LoanDTO>>(await repo.GetFinishedLoansAsync());

        public async Task<LoanDTO> GetLoanAsync(int id)
            => Mapper.Map<LoanDTO>(await repo.GetLoanAsync(id));

        public async Task<IEnumerable<LoanDTO>> GetLoansAsync()
            => Mapper.Map<IEnumerable<LoanDTO>>(await repo.GetLoansAsync());

        public async Task<int> InsertLoanAsync(LoanDTO loan)
        {
            Validate(loan);

            return await repo.InsertLoanAsync(new Loan
            {
                EmployeeId = loan.EmployeeId,
                DateFrom = loan.DateFrom,
                DateTo = loan.DateTo,
                RecipientId = loan.RecipientId
            });
        }

        public async Task UpdateLoanAsync(int id, LoanDTO loan)
            => await repo.UpdateLoanAsync(id, Mapper.Map<Loan>(loan));

        private void Validate(LoanDTO loan)
        {
            if (loan is null)
                throw new ArgumentNullException(nameof(loan));

            if (loan.DateFrom > loan.DateTo)
                throw new ArgumentException($"{nameof(loan.DateFrom)} cannot be greater than {nameof(loan.DateTo)}");
        }
    }
}