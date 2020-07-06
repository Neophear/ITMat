using System.Collections.Generic;
using System.Threading.Tasks;
using ITMat.Data.DTO;
using ITMat.Data.Interfaces;
using ITMat.Data.Repositories.Entities;
using Microsoft.Extensions.Configuration;

namespace ITMat.Data.Repositories
{
    internal class EmployeeRepository : AbstractDapperRepository<Employee, EmployeeDTO>, IEmployeeRepository
    {
        #region Queries
        private const string SqlGetEmployees = "select * from employee;";
        private const string SqlGetEmployee = "select * from employee where [Id] = @Id;";
        private const string SqlFindEmployee = "select * from employee where [MANR] = @MANR;";
        private const string SqlInsertEmployee = "insert into Employee ([MANR], [Name]) values(@MANR, @Name);select SCOPE_IDENTITY();";
        private const string SqlUpdateEmployee = "update Employee set [MANR] = @MANR, [Name] = @Name, [Blacklisted] = @Blacklisted where [Id] = @Id;";
        #endregion

        public EmployeeRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task<EmployeeDTO> FindEmployeeAsync(string manr)
            => await QuerySingleAsync(SqlFindEmployee, new { MANR = manr });

        public async Task<EmployeeDTO> GetEmployeeAsync(int id)
            => await QuerySingleAsync(SqlGetEmployee, new { Id = id });

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
            => await QueryMultipleAsync(SqlGetEmployees);

        public async Task<int> InsertEmployee(EmployeeDTO employee)
            => await QuerySingleAsync<int>(SqlInsertEmployee, new { employee.MANR, employee.Name });

        public async Task UpdateEmployee(int id, EmployeeDTO employee)
            => await QuerySingleAsync(SqlUpdateEmployee, new { Id = id, employee.MANR, employee.Name, employee.Blacklisted });
    }
}