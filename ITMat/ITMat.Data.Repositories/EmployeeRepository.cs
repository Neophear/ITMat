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
        private const string SqlGetEmployees = "select * from employee";
        private const string SqlGetEmployee = "select * from employee where [Id] = @Id";
        private const string SqlFindEmployee = "select * from employee where [MANR] = @MANR";
        #endregion

        public EmployeeRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task<EmployeeDTO> FindEmployeeAsync(string manr)
            => await QuerySingleAsync(SqlFindEmployee, new { MANR = manr });

        public async Task<EmployeeDTO> GetEmployeeAsync(int id)
            => await QuerySingleAsync(SqlGetEmployee, new { Id = id });

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
            => await QueryMultipleAsync(SqlGetEmployees);
    }
}