using ITMat.Core.Data.Interfaces;
using ITMat.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMat.Core.Data.Repositories
{
    public class LoanRepository : AbstractRepository<Loan>, ILoanRepository
    {
        #region Queries
                             //Get
        private const string SqlGetLoans = "select * from vw_loans l",
                             SqlGetActiveLoans = SqlGetLoans + " where active = 1",
                             SqlGetFinishedLoans = SqlGetLoans + " where active = 0",
                             SqlGetLoan = SqlGetLoans + " where id = @id",
                             
                             SqlGetEmployeeLoans = SqlGetLoans + " where employee_id = @employeeId",

                             SqlGetLoanLineItems = "select * from loanlineitem l inner join item i on l.item_id = i.id where l.loan_id = @loanId",
                             SqlGetLoanLineGenericItems = "select * from loanlinegenericitem where loan_id = @loanId",

                             //Insert
                             SqlInsertLoan = "insert into loan ([datefrom], [dateto], [employee_id], [recipient_id], [note]) values(@datefrom, @dateto, @employeeId, @recipientId, @note);" +
                                             "select scope_identity();",
                             SqlInsertLoanLineItem = "insert into loanlineitem (item_id, loan_id) values(@itemId, @loanId)",
                             SqlInsertLoanLineGenericItem = "insert into loanlinegenericitem (genericitem_id, loan_id) values(@genericItemId, @loanId)",

                             //Update (loan_id is also checked on linechanges for extra safety)
                             SqlUpdateLoan = "update loan set [datefrom] = @datefrom, [dateto] = @dateto, [employee_id] = @employeeId, [recipient_id] = @recipientId, [note] = @note where [Id] = @loanId",
                             SqlUpdateLoanLineItem = "update loanlineitem set [pickedup] = @pickedup, [returned] = @returned where [id] = @id and loan_id = @loanId",
                             SqlUpdateLoanLineGenericItem = "update loanlinegenericitem set [pickedup] = @pickedup, [returned] = @returned where [id] = @id and loan_id = @loanId",

                             //Delete (loan_id is also checked on linechanges for extra safety)
                             SqlDeleteLoan = "delete from loan where id = @id",
                             SqlDeleteLoanLineItems = "delete from loanlineitem where loan_id = @loanId and id in @ids",
                             SqlDeleteLoanLineGenericItems = "delete from loanlinegenericitem where loan_id = @loanId and id in @ids";
        #endregion

        public LoanRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task<IEnumerable<Loan>> GetLoansAsync()
            => await QueryMultipleAsync(SqlGetLoans);

        public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
            => await QueryMultipleAsync(SqlGetActiveLoans);

        public async Task<IEnumerable<Loan>> GetFinishedLoansAsync()
            => await QueryMultipleAsync(SqlGetFinishedLoans);

        public async Task<Loan> GetLoanAsync(int id)
            => await QuerySingleAsync(SqlGetLoan);

        public async Task<IEnumerable<Loan>> GetEmployeeLoansAsync(int employeeId)
            => await QueryMultipleAsync(SqlGetEmployeeLoans, new { employeeId });

        public async Task<IEnumerable<LoanLineItem>> GetLoanLineItemsAsync(int loanId)
            => await QueryMultipleAsync<LoanLineItem>(SqlGetLoanLineItems, new { loanId });

        public async Task<IEnumerable<LoanLineGenericItem>> GetLoanLineGenericItemsAsync(int loanId)
            => await QueryMultipleAsync<LoanLineGenericItem>(SqlGetLoanLineGenericItems, new { loanId });

        public async Task<int> InsertLoanAsync(Loan loan, IEnumerable<int> itemIds, IEnumerable<int> genericItemIds)
            => await Transaction(async transaction =>
            {
                var loanId = await QuerySingleAsync<int>(SqlInsertLoan, new { loan.DateFrom, loan.DateTo, loan.EmployeeId, loan.RecipientId, loan.Note });
                
                foreach (var itemId in itemIds)
                    await ExecuteAsync(SqlInsertLoanLineItem, new { itemId, loanId });

                foreach (var genericItemId in genericItemIds)
                    await ExecuteAsync(SqlInsertLoanLineGenericItem, new { genericItemId, loanId });

                return loanId;
            });

        public async Task UpdateLoanAsync(int loanId, Loan loan)
        {
            await Transaction(async transaction =>
            {
                var rowsAffected = await ExecuteAsync(SqlUpdateLoan, new { loanId, loan.DateFrom, loan.DateTo, loan.EmployeeId, loan.RecipientId, loan.Note });

                if (rowsAffected != 1)
                    throw new KeyNotFoundException($"Could not find loan with id {loanId}");

                //Build lists of existing lineIds
                var existingloanlineitemids = (await GetLoanLineItemsAsync(loanId)).Select(line => line.Id);
                var existingloanlinegenericitemids = (await GetLoanLineGenericItemsAsync(loanId)).Select(line => line.Id);

                //Build lists of updated lineIds
                var updatedloanlineitemids = loan.ItemLines.Select(line => line.Id);
                var updatedloanlinegenericitemids = loan.GenericItemLines.Select(line => line.Id);

                //Delete removed lines (existing ids that aren't in updated ids)
                var lineItemsToDelete = existingloanlineitemids.Where(id => !updatedloanlineitemids.Contains(id));
                if (lineItemsToDelete.Count() > 0)
                    rowsAffected += await ExecuteAsync(SqlDeleteLoanLineItems, new { loanId, ids = lineItemsToDelete });

                var lineGenericItemsToDelete = existingloanlinegenericitemids.Where(id => !updatedloanlinegenericitemids.Contains(id));
                if (lineGenericItemsToDelete.Count() > 0)
                    rowsAffected += await ExecuteAsync(SqlDeleteLoanLineGenericItems, new { loanId, ids = lineGenericItemsToDelete });

                //Update existing lines (ids that are on both existing and updated lists
                foreach (var line in loan.ItemLines.Where(line => existingloanlineitemids.Contains(line.Id)))
                {
                    if (1 != await ExecuteAsync(SqlUpdateLoanLineItem, new { line.Id, loanId, line.PickedUp, line.Returned }))
                        throw new KeyNotFoundException($"Could not find loanLineItem with id {line.Id}");

                    rowsAffected++;
                }

                foreach (var line in loan.GenericItemLines.Where(line => existingloanlinegenericitemids.Contains(line.Id)))
                {
                    if (1 != await ExecuteAsync(SqlUpdateLoanLineGenericItem, new { line.Id, loanId, line.PickedUp, line.Returned }))
                        throw new KeyNotFoundException($"Could not find loanLineGenericItem with id {line.Id}");

                    rowsAffected++;
                }

                //Insert new lines (lines with id == 0)
                foreach (var line in loan.ItemLines.Where(line => line.Id == 0))
                    rowsAffected += await ExecuteAsync(SqlInsertLoanLineItem, new { itemId = line.Item.Id, loanId, line.PickedUp, line.Returned });

                foreach (var line in loan.GenericItemLines.Where(line => line.Id == 0))
                    rowsAffected += await ExecuteAsync(SqlInsertLoanLineGenericItem, new { itemId = line.GenericItem.Id, loanId, line.PickedUp, line.Returned });

                return rowsAffected;
            });
        }

        public async Task UpdateLoanLineItemAsync(int id, int loanId, DateTime? pickedUp, DateTime? returned)
        {
            var rowsAffected = await ExecuteAsync(SqlUpdateLoanLineItem, new { id, pickedUp, returned, loanId });

            if (rowsAffected == 0)
                throw new KeyNotFoundException($"Could not find itemLine with id {id} in loan {loanId}.");
        }

        public async Task UpdateLoanLineGenericItemAsync(int id, int loanId, DateTime? pickedUp, DateTime? returned)
        {
            var rowsAffected = await ExecuteAsync(SqlUpdateLoanLineGenericItem, new { id, pickedUp, returned, loanId });

            if (rowsAffected == 0)
                throw new KeyNotFoundException($"Could not find genericItemLine with id {id} in loan {loanId}.");
        }

        public async Task DeleteLoanAsync(int loanId)
        {
            var rowsAffected = await ExecuteAsync(SqlDeleteLoan, new { loanId });

            if (rowsAffected == 0)
                throw new KeyNotFoundException($"Could not find loan with id {loanId}.");
        }

        public async Task DeleteLoanLineItemAsync(int id, int loanId)
        {
            var rowsAffected = await ExecuteAsync(SqlDeleteLoanLineItems, new { id, loanId });

            if (rowsAffected == 0)
                throw new KeyNotFoundException($"Could not find loanlineitem with id {id} in loan {loanId}.");
        }

        public async Task DeleteLoanLineGenericItemAsync(int id, int loanId)
        {
            var rowsAffected = await ExecuteAsync(SqlDeleteLoanLineGenericItems, new { id, loanId });

            if (rowsAffected == 0)
                throw new KeyNotFoundException($"Could not find loanlinegenericitem with id {id} in loan {loanId}.");
        }
    }
}