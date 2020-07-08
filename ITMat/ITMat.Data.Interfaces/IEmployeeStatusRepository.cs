using ITMat.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Data.Interfaces
{
    public interface IEmployeeStatusRepository
    {
        Task<IEnumerable<EmployeeStatusDTO>> GetEmployeeStatusesAsync();
    }
}