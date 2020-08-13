using ITMat.Core.Data.Interfaces;
using ITMat.Core.DTO;
using ITMat.Core.Interfaces;
using ITMat.Core.Models;
using System;
using System.Collections.Generic;
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

        public async Task<int> InsertLoanAsync(CreateLoanDTO dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.EmployeeId <= 0)
                throw new ArgumentException($"{nameof(dto.EmployeeId)} cannot be less than 1.");

            if (dto.DateFrom > dto.DateTo)
                throw new ArgumentException($"{nameof(dto.DateFrom)} cannot be greater than {nameof(dto.DateTo)}.");

            return await repo.InsertLoanAsync(
                new Loan
                {
                    EmployeeId = dto.EmployeeId,
                    DateFrom = dto.DateFrom,
                    DateTo = dto.DateTo,
                    RecipientId = dto.RecipientId,
                    Note = dto.Note
                },
                dto.ItemIds ?? new int[0],
                dto.GenericItemIds ?? new int[0]
            );
        }

        public async Task UpdateLoanAsync(int id, LoanDTO dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.EmployeeId <= 0)
                throw new ArgumentException($"{nameof(dto.EmployeeId)} cannot be less than 1.");

            if (dto.DateFrom > dto.DateTo)
                throw new ArgumentException($"{nameof(dto.DateFrom)} cannot be greater than {nameof(dto.DateTo)}.");

            await repo.UpdateLoanAsync(id, Mapper.Map<Loan>(dto));
        }

        public async Task DeleteLoanAsync(int id)
            => await repo.DeleteLoanAsync(id);

        public async Task UpdateLoanLineItemAsync(int id, int loanId, DateTime? pickedUp, DateTime? returned)
            => await repo.UpdateLoanLineItemAsync(id, loanId, pickedUp, returned);

        public async Task UpdateLoanLineGenericItemAsync(int id, int loanId, DateTime? pickedUp, DateTime? returned)
            => await repo.UpdateLoanLineGenericItemAsync(id, loanId, pickedUp, returned);

        public async Task DeleteLoanLineItemAsync(int id, int loanId)
            => await repo.DeleteLoanLineItemAsync(id, loanId);

        public async Task DeleteLoanLineGenericItemAsync(int id, int loanId)
            => await repo.DeleteLoanLineGenericItemAsync(id, loanId);
    }
}