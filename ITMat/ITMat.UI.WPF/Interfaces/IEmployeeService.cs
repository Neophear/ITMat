using ITMat.UI.WPF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WPF.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}