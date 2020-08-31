using ITMat.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetEmployeeAsync(int id);
        Task<EmployeeDTO> FindEmployeeAsync(string manr);
        Task<IEnumerable<EmployeeListedDTO>> GetEmployeesAsync();
        Task<int> InsertEmployeeAsync(EmployeeDTO employee);
        Task UpdateEmployeeAsync(int id, EmployeeDTO employee);

        Task<IEnumerable<EmployeeStatusDTO>> GetEmployeeStatusesAsync();
    }
}