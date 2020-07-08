using ITMat.Core.Interfaces;
using ITMat.Data.DTO;
using ITMat.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Services
{
    public class EmployeeStatusService : IEmployeeStatusService
    {
        private readonly IEmployeeStatusRepository repo;

        public EmployeeStatusService(IEmployeeStatusRepository repo)
            => this.repo = repo;

        public async Task<IEnumerable<EmployeeStatusDTO>> GetEmployeeStatusesAsync()
            => await repo.GetEmployeeStatusesAsync();
    }
}