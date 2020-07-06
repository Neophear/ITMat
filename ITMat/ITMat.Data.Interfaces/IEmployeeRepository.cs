using ITMat.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDTO> GetEmployeeAsync(int id);
        Task<EmployeeDTO> FindEmployeeAsync(string manr);
        Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync();
    }
}