using ITMat.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployeeAsync(EmployeeDTO employee);

        Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeAsync(int id);
        Task<IEnumerable<EmployeeStatusDTO>> GetStatusesAsync();

        Task UpdateEmployeeAsync(int id, EmployeeDTO employee);
    }
}