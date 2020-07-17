using ITMat.Core.Data.Interfaces;
using ITMat.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Data.Repositories
{
    public class EmployeeRepository : AbstractRepository<Employee>, IEmployeeRepository
    {
        #region Queries
        private const string SqlGetEmployees = "select * from employee e inner join employeestatus s on e.status_id = s.id",
                             SqlGetEmployee = "select * from employee e inner join employeestatus s on e.status_id = s.id where e.[id] = @id",
                             SqlFindEmployee = "select * from employee e inner join employeestatus s on e.status_id = s.id where e.[manr] = @manr",
                             SqlInsertEmployee = "insert into employee ([manr], [name]) values(@manr, @name);select scope_identity()",
                             SqlUpdateEmployee = "update employee set [manr] = @manr, [name] = @name, status_id = @statusid where [Id] = @Id",
                             SqlGetEmployeeStatuses = "select * from employeestatus";
        #endregion

        public EmployeeRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task<Employee> FindEmployeeAsync(string manr)
            => await QuerySingleAsync<EmployeeStatus>(SqlFindEmployee, MapFromSql, new { manr });

        public async Task<Employee> GetEmployeeAsync(int id)
            => await QuerySingleAsync<EmployeeStatus>(SqlGetEmployee, MapFromSql, new { id });

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
            => await QueryMultipleAsync<EmployeeStatus>(SqlGetEmployees, MapFromSql);

        public async Task<IEnumerable<EmployeeStatus>> GetEmployeeStatusesAsync()
            => await QueryMultipleAsync<EmployeeStatus>(SqlGetEmployeeStatuses);

        public async Task<int> InsertEmployee(Employee employee)
            => await QuerySingleAsync<int>(SqlInsertEmployee, new { employee.MANR, employee.Name });

        public async Task UpdateEmployee(int id, Employee employee)
        {
            var rowsAffected = await ExecuteAsync(SqlUpdateEmployee, new { id, employee.MANR, employee.Name, statusid = employee.Status.Id });

            if (rowsAffected != 1)
                throw new KeyNotFoundException($"Could not find employee with id {id}");
        }

        private Employee MapFromSql(Employee employee, EmployeeStatus status)
        {
            employee.Status = status;
            return employee;
        }
    }
}