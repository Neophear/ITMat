using ITMat.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployeeAsync(EmployeeDTO employee);

        Task<IEnumerable<EmployeeListedDTO>> GetEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeAsync(int id);
        Task<IEnumerable<EmployeeStatusDTO>> GetStatusesAsync();

        Task UpdateEmployeeAsync(int id, EmployeeDTO employee);
    }
}