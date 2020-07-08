using ITMat.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetEmployeeAsync(int id);
        Task<EmployeeDTO> FindEmployeeAsync(string manr);
        Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync();
        Task<int> InsertEmployeeAsync(EmployeeDTO employee);
        Task UpdateEmployeeAsync(int id, EmployeeDTO employee);
    }
}