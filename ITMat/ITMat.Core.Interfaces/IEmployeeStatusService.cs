using ITMat.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Interfaces
{
    public interface IEmployeeStatusService
    {
        Task<IEnumerable<EmployeeStatusDTO>> GetEmployeeStatusesAsync();
    }
}