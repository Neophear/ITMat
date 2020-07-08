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
        private const string    SqlGetEmployees = "select * from employee e inner join employeestatus s on e.status_id = s.id;",
                                SqlGetEmployee = "select * from employee e inner join employeestatus s on e.status_id = s.id where e.[id] = @id;",
                                SqlFindEmployee = "select * from employee e inner join employeestatus s on e.status_id = s.id where e.[manr] = @manr;",
                                SqlInsertEmployee = "insert into employee ([manr], [name]) values(@manr, @name);select scope_identity();",
                                SqlUpdateEmployee = "update employee set [manr] = @manr, [name] = @name, status_id = @statusid where [Id] = @Id;";
        #endregion

        public EmployeeRepository(IConfiguration configuration)
            : base(configuration, MapperFactory.Create<EmployeeProfile>()) { }

        public async Task<EmployeeDTO> FindEmployeeAsync(string manr)
            => await QuerySingleAsync<EmployeeStatus>(SqlFindEmployee, MapFromSql, new { manr });

        public async Task<EmployeeDTO> GetEmployeeAsync(int id)
            => await QuerySingleAsync<EmployeeStatus>(SqlGetEmployee, MapFromSql, new { id });

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
            => await QueryMultipleAsync<EmployeeStatus>(SqlGetEmployees, MapFromSql);

        public async Task<int> InsertEmployee(EmployeeDTO employee)
            => await QuerySingleAsync<int>(SqlInsertEmployee, new { employee.MANR, employee.Name });

        public async Task UpdateEmployee(int id, EmployeeDTO employee)
            => await QuerySingleAsync(SqlUpdateEmployee, new { id, employee.MANR, employee.Name, statusid = employee.Status.Id });

        private Employee MapFromSql(Employee employee, EmployeeStatus status)
        {
            employee.Status = status;
            return employee;
        }

        private class EmployeeProfile : GenericProfile
        {
            public EmployeeProfile()
            {
                CreateMap<Employee, EmployeeDTO>().ReverseMap();
                CreateMap<EmployeeStatus, EmployeeStatusDTO>().ReverseMap();
            }
        }
    }
}