using ITMat.Core.Data.Interfaces;
using ITMat.Core.DTO;
using ITMat.Core.Interfaces;
using ITMat.Core.Models;
using ITMat.Core.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMat.Core.Services
{
    public class LoanService : AbstractService<Loan, LoanDTO>, ILoanService
    {
        private readonly ILoanRepository loanRepo;
        private readonly IEmployeeRepository employeeRepo;

        public LoanService(ILoanRepository loanRepo, IEmployeeRepository employeeRepo)
        {
            this.loanRepo = loanRepo;
            this.employeeRepo = employeeRepo;
        }

        public async Task<IEnumerable<LoanDTO>> GetActiveLoansAsync()
            => Mapper.Map<IEnumerable<LoanDTO>>(await loanRepo.GetActiveLoansAsync());

        public async Task<IEnumerable<LoanDTO>> GetFinishedLoansAsync()
            => Mapper.Map<IEnumerable<LoanDTO>>(await loanRepo.GetFinishedLoansAsync());

        public async Task<LoanDTO> GetLoanAsync(int id)
            => Mapper.Map<LoanDTO>(await loanRepo.GetLoanAsync(id));

        public async Task<IEnumerable<LoanDTO>> GetLoansAsync()
            => Mapper.Map<IEnumerable<LoanDTO>>(await loanRepo.GetLoansAsync());

        public async Task<IEnumerable<LoanListedDTO>> GetEmployeeLoansAsync(int employeeId)
             => (await loanRepo.GetEmployeeLoansAsync(employeeId))
                .Select(l => new LoanListedDTO
                {
                    Id = l.Id,
                    DateFrom = l.DateFrom,
                    DateTo = l.DateTo,
                    Active = l.Active
                });

        public async Task<IEnumerable<LoanEmployeeListedDTO>> GetLoansListedAsync()
        {
            var loans = Mapper.Map<IEnumerable<LoanDTO>>(await loanRepo.GetLoansAsync());

            var loanListed = new List<LoanEmployeeListedDTO>(loans.Count());

            foreach (var loan in loans)
            {
                var employee = await employeeRepo.GetEmployeeAsync(loan.EmployeeId);

                loanListed.Add(new LoanEmployeeListedDTO
                {
                    Id = loan.Id,
                    MANR = employee.MANR,
                    Name = employee.Name,
                    DateFrom = loan.DateFrom,
                    DateTo = loan.DateTo,
                    Active = loan.Active
                });
            }

            return loanListed;
        }

        public async Task<int> InsertLoanAsync(CreateLoanDTO dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.EmployeeId <= 0)
                throw new ArgumentException($"{nameof(dto.EmployeeId)} cannot be less than 1.");

            var employee = await employeeRepo.GetEmployeeAsync(dto.EmployeeId);

            if (employee is null)
                throw new KeyNotFoundException($"Could not find employee with Id {dto.EmployeeId}.");
            else if (!employee.Status.CanLend)
                throw new LendingProhibitedException();

            if (dto.DateFrom > dto.DateTo)
                throw new ArgumentException($"{nameof(dto.DateFrom)} cannot be greater than {nameof(dto.DateTo)}.");

            return await loanRepo.InsertLoanAsync(
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

            await loanRepo.UpdateLoanAsync(id, Mapper.Map<Loan>(dto));
        }

        public async Task DeleteLoanAsync(int id)
            => await loanRepo.DeleteLoanAsync(id);

        public async Task UpdateLoanLineItemAsync(int id, int loanId, DateTime? pickedUp, DateTime? returned)
            => await loanRepo.UpdateLoanLineItemAsync(id, loanId, pickedUp, returned);

        public async Task UpdateLoanLineGenericItemAsync(int id, int loanId, DateTime? pickedUp, DateTime? returned)
            => await loanRepo.UpdateLoanLineGenericItemAsync(id, loanId, pickedUp, returned);

        public async Task DeleteLoanLineItemAsync(int id, int loanId)
            => await loanRepo.DeleteLoanLineItemAsync(id, loanId);

        public async Task DeleteLoanLineGenericItemAsync(int id, int loanId)
            => await loanRepo.DeleteLoanLineGenericItemAsync(id, loanId);
    }
}