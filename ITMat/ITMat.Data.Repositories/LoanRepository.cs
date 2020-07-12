using ITMat.Data.DTO;
using ITMat.Data.Interfaces;
using ITMat.Data.Repositories.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Data.Repositories
{
    internal class LoanRepository : AbstractDapperRepository<Loan, LoanDTO>, ILoanRepository
    {
        #region Queries
        private const string SqlGetLoans = "select * from loans l inner join loanstatus s on l.status_id = s.id",
                             SqlGetActiveLoans = SqlGetLoans + " where status_id = 1",
                             SqlGetFinishedLoans = SqlGetLoans + " where status_id = 2",
                             SqlGetLoan = SqlGetLoans + " where l.[id] = @id",
                             SqlInsertLoan =    "insert into laon ([datefrom], [dateto], [employee_id], [recipient_id], [note]) values(@datefrom, @dateto, @employeeId, @recipientId, @note);" +
                                                "select scope_identity();",
                             SqlUpdateLoan = "update loan set [datefrom] = @datefrom, [dateto] = @dateto, [employee_id] = @employeeId, [recipient_id] = @recipientId, status_id = @statusid, [note] = @note where [Id] = @Id";
        #endregion

        public LoanRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task<IEnumerable<LoanDTO>> GetLoansAsync()
            => await QueryMultipleAsync<LoanStatus>(SqlGetLoans, MapFromSql);

        public async Task<IEnumerable<LoanDTO>> GetActiveLoansAsync()
            => await QueryMultipleAsync<LoanStatus>(SqlGetActiveLoans, MapFromSql);

        public async Task<IEnumerable<LoanDTO>> GetFinishedLoansAsync()
            => await QueryMultipleAsync<LoanStatus>(SqlGetFinishedLoans, MapFromSql);

        public async Task<LoanDTO> GetLoanAsync(int id)
            => await QuerySingleAsync<LoanStatus>(SqlGetLoan, MapFromSql);

        public async Task<int> InsertLoanAsync(LoanDTO loan)
            => await QuerySingleAsync<int>(SqlInsertLoan, new { loan.DateFrom, loan.DateTo, loan.EmployeeId, loan.RecipientId, loan.Note });

        public async Task UpdateLoanAsync(int id, LoanDTO loan)
            => await QuerySingleAsync(SqlUpdateLoan, new { id, loan.DateFrom, loan.DateTo, loan.EmployeeId, loan.RecipientId, loan.Status.Id, loan.Note });

        private Loan MapFromSql(Loan loan, LoanStatus status)
        {
            loan.Status = status;
            return loan;
        }

        private class LoanProfile : GenericProfile
        {
            public LoanProfile()
            {
                CreateMap<Loan, LoanDTO>().ReverseMap();
                CreateMap<LoanStatus, LoanStatusDTO>().ReverseMap();
            }
        }
    }
}