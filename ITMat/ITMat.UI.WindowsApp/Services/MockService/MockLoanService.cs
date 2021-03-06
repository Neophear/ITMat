﻿using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services.MockService
{
    public class MockLoanService : AbstractMockService, ILoanService
    {
        private readonly List<LoanDTO> loans;
        private readonly IEnumerable<EmployeeDTO> employees = new EmployeeDTO[3]
            {
                new EmployeeDTO{ Id = 1, MANR = "370929", Name = "Stiig Gade", Status = 1 },
                new EmployeeDTO{ Id = 2, MANR = "123456", Name = "Peter Petersen", Status = 1 },
                new EmployeeDTO{ Id = 3, MANR = "654321", Name = "Jens Jensen", Status = 2 }
            };

        public MockLoanService()
        {
            loans = new List<LoanDTO>();

            for (int i = 1; i <= 10; i++)
            {
                var startDate = DateTime.Today.AddMonths(i - 3);
                var endDate = startDate.AddDays(30);

                var loan = new LoanDTO
                {
                    Id = i,
                    DateFrom = startDate,
                    DateTo = endDate,
                    EmployeeId = (i % 3) + 1,
                    Note = "Autogenerated Loan"
                };

                var itemLines = new List<LoanLineItemDTO>(i);

                for (int j = 0; j < i; j++)
                {
                    itemLines.Add(new LoanLineItemDTO
                    {
                        Id = i * j,
                        PickedUp = startDate,
                        Returned = i % 2 == 0 ? (DateTime?)startDate.AddDays(1) : null
                    });
                }

                loan.Active = itemLines.Any(l => l.Returned == null);

                loan.ItemLines = itemLines;

                loans.Add(loan);
            }
        }

        private LoanDTO CopyLoan(LoanDTO loan)
            => new LoanDTO
            {
                Id = loan.Id,
                Active = loan.Active,
                DateFrom = loan.DateFrom,
                DateTo = loan.DateTo,
                EmployeeId = loan.EmployeeId,
                Note = loan.Note,
                RecipientId = loan.RecipientId,
                ItemLines = loan.ItemLines.ToList(),
                GenericItemLines = loan.GenericItemLines.ToList()
            };

        public async Task<IEnumerable<LoanListedDTO>> GetEmployeeLoansAsync(int employeeId)
            => await ExecuteWithDelay(() =>
                loans
                .Where(l => l.EmployeeId == employeeId)
                .Select(l => new LoanListedDTO
                {
                    Id = l.Id,
                    DateFrom = l.DateFrom,
                    DateTo = l.DateTo,
                    Active = l.Active
                }));

        public async Task<LoanDTO> GetLoanAsync(int id)
            => await ExecuteWithDelay(() => CopyLoan(loans.FirstOrDefault(l => l.Id == id)));

        public async Task<IEnumerable<LoanEmployeeListedDTO>> GetLoansAsync()
            => await ExecuteWithDelay(() => loans.Select(l =>
            {
                var employee = employees.FirstOrDefault(e => e.Id == l.EmployeeId);

                return new LoanEmployeeListedDTO
                {
                    Id = l.Id,
                    MANR = employee.MANR,
                    Name = employee.Name,
                    DateFrom = l.DateFrom,
                    DateTo = l.DateTo,
                    Active = l.Active
                };
            }));
    }
}