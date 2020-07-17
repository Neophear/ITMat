using ITMat.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeAsync(int id);
        Task<Employee> FindEmployeeAsync(string manr);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<IEnumerable<EmployeeStatus>> GetEmployeeStatusesAsync();
        Task<int> InsertEmployee(Employee employee);
        Task UpdateEmployee(int id, Employee employee);
    }
}